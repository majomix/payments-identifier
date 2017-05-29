/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.Model;
using Prism.Events;

namespace PaymentsIdentifier.Events
{
    internal class ShowInsertInputEvent : PubSubEvent<Payment> { }
}
