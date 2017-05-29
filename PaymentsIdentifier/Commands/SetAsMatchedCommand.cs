/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.Events;
using PaymentsIdentifier.Model;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace PaymentsIdentifier.Commands
{
    [Export]
    internal class SetAsMatchedCommand : ICommand
    {
        private IEventAggregator myEventAggregator;

        [ImportingConstructor]
        public SetAsMatchedCommand(IEventAggregator eventAggregator)
        {
            myEventAggregator = eventAggregator;
        }

        public bool CanExecute(object parameter)
        {
            var p = parameter as object[];
            if (p == null) return false;

            Payment selectedPayment = p[0] as Payment;
            ObservableCollection<Invoice> selectedList = p[1] as ObservableCollection<Invoice>;
            if(selectedPayment == null || selectedList == null) return false;

            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            object[] parameters = (object[])parameter;
            Payment payment = (Payment)parameters[0];
            ObservableCollection<Invoice> selectedList = (ObservableCollection<Invoice>)parameters[1];

            payment.FinalMatchedInvoices = selectedList;

            myEventAggregator.GetEvent<InvoiceDetailsChangedEvent>().Publish("");
        }
    }
}
