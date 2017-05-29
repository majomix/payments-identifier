/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.Model;
using PaymentsIdentifier.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PaymentsIdentifier.Commands
{
    [Export]
    internal class LoadExcelFilesCommand : AbstractAsynchronousCommand
    {
        private IdentifierFacade myFacade;

        [ImportingConstructor]
        public LoadExcelFilesCommand(IdentifierFacade facade)
        {
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
            Func<IProgress<int>, CancellationToken, Task<int>> loadFilesAction = ((progress, token) => LoadFiles(vm.DailyReportFilePath, vm.UnallocatedCashReportFilePath, progress, token));
            CancellationTokenSource timerTokenSource = new CancellationTokenSource();
            Action<IProgress<int>, CancellationToken> action2 = ((progress, token) => ProgressUpdateByTime(progress, token));
            Task timerTask = Task.Run(() => action2(vm.Progress, timerTokenSource.Token), timerTokenSource.Token);
            Execution = new NotifyTaskCompletion<int>(loadFilesAction(vm.Progress, new CancellationToken()));
            RaiseCanExecuteChanged();
            await Execution.TaskCompletion;
            timerTokenSource.Cancel();
            timerTask.Wait();
            RaiseCanExecuteChanged();
            int d = Execution.Result;
        }

        public async Task<int> LoadFiles(string dailyReportFilePath, string unallocatedReportFilePath, IProgress<int> progress, CancellationToken token = new CancellationToken())
        {
            ExcelLoader excelLoader = new ExcelLoader(dailyReportFilePath, unallocatedReportFilePath);

            await Task.Run(() => {
                try {
                    lock (myFacade.CollectionLock)
                    {
                        myFacade.Invoices = excelLoader.LoadInvoices();
                    } 
                }
                catch(Exception e) { MessageBox.Show(e.Message); }
            }, token);
            progress.Report(60);

            IEnumerable<string> unallocatedReportSheetNames = excelLoader.GetPaymentsSheetNames();

            int paymentOrdinalNumber = 1;

            foreach (string sheetName in unallocatedReportSheetNames)
            {
                if (ReportMappings.SupportedCountries().Select(_ => _.SheetName).Contains(sheetName))
                {
                    List<Payment> paymentsInSheet = null;
                    List<Payment> paymentsForCountry = new List<Payment>();

                    await Task.Run(() => {
                        try
                        {
                            paymentsInSheet = excelLoader.LoadPaymentsForSheet(sheetName);

                            foreach (Payment payment in paymentsInSheet)
                            {
                                payment.Value = -payment.Value;
                                payment.Country = sheetName;
                                payment.Type = PaymentType.x86;
                                payment.PaymentOrdinalNumber = paymentOrdinalNumber++;

                                myFacade.Payments.Add(payment);
                                paymentsForCountry.Add(payment);
                            }

                            foreach (Payment item in myFacade.Payments)
                            {
                                Customer customerItem = myFacade.CustomerDatabase.Where(customer => customer.Country.SheetName == sheetName && customer.Name == item.CustomerName).SingleOrDefault();
                                
                                if(customerItem != null)
                                {
                                    IEnumerable<Invoice> filteredInvoices = myFacade.Invoices.Where(invoice => invoice.Country == customerItem.Country.Name && customerItem.Identifiers.Contains(invoice.CustomerNumber));
                                    item.Invoices = new ObservableCollection<Invoice>(filteredInvoices);
                                }
                            }
                        }
                        catch (Exception e) { MessageBox.Show(e.Message); }
                    }, token);


                }
            }
            progress.Report(100);

            return 42;
        }

        public void ProgressUpdateByTime(IProgress<int> progress, CancellationToken token = new CancellationToken())
        {
            while(true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                Thread.Sleep(600);
                progress.Report(0);
            }
        }

    }

}
