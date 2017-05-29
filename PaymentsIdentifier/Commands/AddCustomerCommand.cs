/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.Events;
using PaymentsIdentifier.Model;
using Prism.Events;
using System;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace PaymentsIdentifier.Commands
{
    [Export]
    internal class AddCustomerCommand : ICommand
    {
        private IdentifierFacade myFacade;

        [ImportingConstructor]
        public AddCustomerCommand(IdentifierFacade facade)
        {
            myFacade = facade;
        }

        public bool CanExecute(object parameter)
        {
            return parameter is Customer;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            Customer viewCustomer = (Customer)parameter;
            myFacade.CustomerDatabase.Add(viewCustomer.CloneCustomer());
            viewCustomer.Clear();
        }
    }
}
