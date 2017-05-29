/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using System.Threading.Tasks;
using System.Windows.Input;

namespace PaymentsIdentifier.Commands
{
    internal interface IAsynchronousCommand : ICommand
    {
        Task ExecuteAsynchronously(object parameter);
    }
}
