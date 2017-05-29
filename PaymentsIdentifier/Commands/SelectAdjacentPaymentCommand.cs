/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.Events;
using PaymentsIdentifier.Model;
using Prism.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;

namespace PaymentsIdentifier.Commands
{
    [Export]
    internal class SelectAdjacentPaymentCommand : ICommand
    {
        private IEventAggregator myEventAggregator;
        private IdentifierFacade myFacade;

        [ImportingConstructor]
        public SelectAdjacentPaymentCommand(IEventAggregator eventAggregator, IdentifierFacade facade)
        {
            myEventAggregator = eventAggregator;
            myFacade = facade;
        }

        public bool CanExecute(object parameter)
        {
            var p = parameter as object[];
            if (p == null) return false;

            Payment selectedPayment = p[0] as Payment;
            if (selectedPayment == null || !(p[1] is bool) || !(p[2] is bool)) return false;

            return ReturnAdjacentFilteredInvoice(selectedPayment, (bool)p[1], (bool)p[2]) != null;
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
            bool next = (bool)parameters[1];
            bool includeResolved = (bool)parameters[2];

            myEventAggregator.GetEvent<SelectedPaymentChangedEvent>().Publish(ReturnAdjacentFilteredInvoice(selectedPayment, next, includeResolved));
        }

        private Payment ReturnAdjacentFilteredInvoice(Payment payment, bool next, bool includeResolved)
        {
            int selectedIndex = myFacade.Payments.IndexOf(payment);
            
            if (next)
            {
                IEnumerable<Payment> nextPayments = myFacade.Payments.Skip(selectedIndex + 1);
                if (!includeResolved) nextPayments = nextPayments.Where(_ => _.FinalMatchedInvoices.IsNullOrEmpty());

                return nextPayments.FirstOrDefault();
            }
            else
            {
                IEnumerable<Payment> previousPayments = myFacade.Payments.Take(selectedIndex);
                if (!includeResolved) previousPayments = previousPayments.Where(_ => _.FinalMatchedInvoices.IsNullOrEmpty());

                return previousPayments.LastOrDefault();
            }
        }
    }
}
