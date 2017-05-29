/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.Events;
using PaymentsIdentifier.Model;
using PaymentsIdentifier.ViewModel;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;

namespace PaymentsIdentifier.Commands
{
    [Export]
    internal class InsertInvoicesCommand : IInteractionCommand
    {
        private IEventAggregator myEventAggregator;
        private IdentifierFacade myFacade;
        private readonly InteractionRequest<Confirmation> myInteractionRequest = new InteractionRequest<Confirmation>();

        [ImportingConstructor]
        public InsertInvoicesCommand(IEventAggregator eventAggregator, IdentifierFacade facade)
        {
            myEventAggregator = eventAggregator;
            myFacade = facade;
        }

        public IInteractionRequest InteractionRequest
        {
            get { return myInteractionRequest; }
        }

        public bool CanExecute(object parameter)
        {
            var p = parameter as object[];
            if (p == null) return false;

            ObservableCollection<ObservableCollection<Invoice>> matchedList = p[0] as ObservableCollection<ObservableCollection<Invoice>>;
            return matchedList != null;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            var parameters = (object[])parameter;
            ObservableCollection<ObservableCollection<Invoice>> matchedList = (ObservableCollection<ObservableCollection<Invoice>>)parameters[0];
            ObservableCollection<Invoice> selectedMatchedList = (ObservableCollection<Invoice>)parameters[1];

            InsertInvoicesViewModel insertInvoicesViewModel = new InsertInvoicesViewModel();

            myInteractionRequest.Raise(new Confirmation()
            {
                Title = "Insert payment details",
                Content = insertInvoicesViewModel
            },
            _ =>
            {
                if (_.Confirmed)
                {
                    if(insertInvoicesViewModel.UserInput != null)
                    {
                        List<string> invoiceNumbers = insertInvoicesViewModel.UserInput.ExtractInvoiceNumbers();

                        if (!invoiceNumbers.IsNullOrEmpty())
                        {
                            ObservableCollection<Invoice> foundInvoices = new ObservableCollection<Invoice>(myFacade.Invoices.Where(invoice => invoiceNumbers.Contains(invoice.InvoiceNumber)));
                            if (!foundInvoices.IsNullOrEmpty())
                            {
                                if (matchedList.Count == 0)
                                {
                                    matchedList.Add(foundInvoices);
                                    myEventAggregator.GetEvent<SelectedMatchedListChangedEvent>().Publish(foundInvoices);
                                }
                                else
                                {
                                    if (selectedMatchedList == null) return;

                                    foreach(Invoice invoice in foundInvoices)
                                    {
                                        selectedMatchedList.Add(invoice);
                                    }
                                    myEventAggregator.GetEvent<SelectedMatchedListChangedEvent>().Publish(selectedMatchedList);
                                }
                            }
                        }
                    }
                }
            });
        }
    }
}
