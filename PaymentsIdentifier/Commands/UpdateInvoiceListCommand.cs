/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.ViewModel;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace PaymentsIdentifier.Commands
{
    [Export]
    internal class UpdateInvoiceListCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event System.EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            EditAllocationsWindowViewModel p = parameter as EditAllocationsWindowViewModel;
        }
    }
}
