/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.Model;
using Prism.Events;
using System.Collections.ObjectModel;

namespace PaymentsIdentifier.Events
{
    internal class SelectedMatchedListChangedEvent : PubSubEvent<ObservableCollection<Invoice>> { }
}
