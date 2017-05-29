/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.Events;
using PaymentsIdentifier.Model;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;

namespace PaymentsIdentifier.Commands
{
    [Export]
    internal class SelectAdjacentMatchedListCommand : ICommand
    {
        private IEventAggregator myEventAggregator;

        [ImportingConstructor]
        public SelectAdjacentMatchedListCommand(IEventAggregator eventAggregator)
        {
            myEventAggregator = eventAggregator;
        }

        public bool CanExecute(object parameter)
        {
            var p = parameter as object[];
            if (p == null) return false;

            Payment selectedPayment = p[0] as Payment;
            ObservableCollection<Invoice> selectedList = p[1] as ObservableCollection<Invoice>;
            if(selectedPayment == null || selectedList == null || !(p[2] is bool)) return false;

            return ReturnAdjacentMatchedList(selectedPayment, selectedList, (bool)p[2]) != null;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            object[] parameters = (object[])parameter;
            Payment selectedPayment = (Payment)parameters[0];
            ObservableCollection<Invoice> selectedList = (ObservableCollection<Invoice>)parameters[1];
            bool next = (bool)parameters[2];

            myEventAggregator.GetEvent<SelectedMatchedListChangedEvent>().Publish(ReturnAdjacentMatchedList(selectedPayment, selectedList, next));
        }

        private ObservableCollection<Invoice> ReturnAdjacentMatchedList(Payment payment, ObservableCollection<Invoice> list, bool next)
        {
            int selectedIndex = payment.MatchedInvoices.IndexOf(list);
            
            if (next)
            {
                return payment.MatchedInvoices.Skip(selectedIndex + 1).FirstOrDefault();
            }
            else
            {
                return payment.MatchedInvoices.Take(selectedIndex).LastOrDefault();
            }
        }
    }
}
