/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using Prism.Events;

namespace PaymentsIdentifier.Events
{
    internal class InvoiceDetailsChangedEvent : PubSubEvent<string> { }
}
