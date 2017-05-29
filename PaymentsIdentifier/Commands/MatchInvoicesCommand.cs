/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.Events;
using PaymentsIdentifier.Model;
using PaymentsIdentifier.ViewModel;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PaymentsIdentifier.Commands
{
    [Export]
    internal class MatchInvoicesCommand : AbstractAsynchronousCommand
    {
        private IdentifierFacade myFacade;
        private IEventAggregator myEventAggregator;

        [ImportingConstructor]
        public MatchInvoicesCommand(IEventAggregator eventAggregator, IdentifierFacade facade)
        {
            myEventAggregator = eventAggregator;
            myFacade = facade;
            myCancelCommand = new CancelAsynchronousCommand();
        }

        public override bool CanExecute(object parameter)
        {
            return (Execution == null || Execution.IsCompleted) && parameter is MainWindowViewModel;
        }

        public override async Task ExecuteAsynchronously(object parameter)
        {
            var vm = (MainWindowViewModel)parameter;
            Func<IProgress<int>, CancellationToken, Task<int>> loadFilesAction = ((progress, token) => MatchInvoices(vm.Aging, vm.Tolerance, vm.SelectedRegions.Keys.ToArray(), vm.Progress, token));
            CancellationTokenSource timerTokenSource = new CancellationTokenSource();
            Execution = new NotifyTaskCompletion<int>(loadFilesAction(vm.Progress, new CancellationToken()));
            RaiseCanExecuteChanged();
            await Execution.TaskCompletion;
            RaiseCanExecuteChanged();
            int d = Execution.Result;
        }

        public async Task<int> MatchInvoices(int aging, double difference, string[] countries, IProgress<int> progress, CancellationToken token = new CancellationToken())
        {
            foreach(Payment payment in myFacade.Payments)
            {
                if (!payment.MatchedInvoices.IsNullOrEmpty())
                {
                    payment.MatchedInvoices.Clear();
                    payment.FinalMatchedInvoices.Clear();
                    payment.Status = IdentifyStatus.Unidentified;
                }
            }
            foreach (Payment payment in myFacade.Payments.Where(_ => _.Invoices.IsNullOrEmpty() == false && countries.Contains(_.Country)))
            {
                // 1. try to look for invoices in payment details
                List<string> extractedInvoiceNumbers = payment.PaymentDetails.ExtractInvoiceNumbers();
                MatchByInvoiceNumbers(payment, extractedInvoiceNumbers);

                // 2. match by values if there wasn't a complete match
                if (!payment.MatchedInvoices.IsNullOrEmpty() && Math.Abs(payment.MatchedInvoices.First().Select(_ => _.Value).Sum() - payment.Value) < 0.01) continue;

                List<string> allCustomerNumbers = payment.Invoices.Select(_ => _.CustomerNumber).Distinct().ToList();

                // first all invoices, then each customer number separately
                InvoiceFilterChain filter = new InvoiceFilterChain(aging, payment.Value);
                await MatchByValue(filter.FilterInvoices(payment.Invoices), payment, difference);

                if(payment.MatchedInvoices.IsNullOrEmpty())
                {
                    foreach (string customerNumber in allCustomerNumbers)
                    {
                        await MatchByValue(filter.FilterInvoices(payment.Invoices.Where(_ => _.CustomerNumber == customerNumber)), payment, difference);
                    }
                }
            }
            progress.Report(60);

            myEventAggregator.GetEvent<InvoicesMatchedEvent>().Publish("");

            return 42;
        }

        private void MatchByInvoiceNumbers(Payment payment, List<string> extractedInvoiceNumbers)
        {
            if (!extractedInvoiceNumbers.IsNullOrEmpty())
            {
                List<string> remainingInvoiceNumbers = payment.PairExtractedInvoiceNumbers(extractedInvoiceNumbers);
                if (!remainingInvoiceNumbers.IsNullOrEmpty())
                {
                    List<Invoice> remainingInvoices = myFacade.Invoices.Where(_ => remainingInvoiceNumbers.Contains(_.InvoiceNumber)).ToList();
                    if (!remainingInvoices.IsNullOrEmpty())
                    {
                        payment.AddInvoicesForUnmatchedCustomer(remainingInvoices);
                    }
                }
            }
        }

        private async Task MatchByValue(IEnumerable<Invoice> filteredInvoices, Payment payment, double difference)
        {
            InvoiceMatcher invoiceMatcher = new InvoiceMatcher(filteredInvoices.Select(_ => _.Value).ToList(), payment.Value);

            await Task.Run(() =>
            {
                try
                {
                    invoiceMatcher.Match(difference);
                    payment.PairMatchedInvoiceValues(invoiceMatcher.MatchesFound);
                }
                catch (Exception e) { MessageBox.Show(e.Message); }
            });
        }
    }
}
