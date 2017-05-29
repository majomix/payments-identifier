/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using Prism.Interactivity.InteractionRequest;
using System.Windows.Input;

namespace PaymentsIdentifier.Commands
{
    internal interface IInteractionCommand : ICommand
    {
        IInteractionRequest InteractionRequest { get; }
    }
}
