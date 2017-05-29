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
    internal class InsertInputCommand : ICommand
    {
        private IEventAggregator myEventAggregator;

        [ImportingConstructor]
        public InsertInputCommand(IEventAggregator eventAggregator)
        {
            myEventAggregator = eventAggregator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            Payment payment = (Payment)parameter;
            myEventAggregator.GetEvent<ShowInsertInputEvent>().Publish(payment);
        }
    }
}
