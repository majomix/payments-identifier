/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PaymentsIdentifier.Model
{
    internal abstract class CustomerDatabaseCreatorAbstract
    {
        public ObservableCollection<Customer> CustomerDatabase { get; private set; }

        protected CustomerDatabaseCreatorAbstract()
        {
            CustomerDatabase = new ObservableCollection<Customer>();
        }
    }
}