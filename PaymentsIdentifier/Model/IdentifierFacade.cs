/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Data;

namespace PaymentsIdentifier.Model
{
    [Export]
    internal class IdentifierFacade
    {
        public ObservableCollection<Customer> CustomerDatabase { get; private set; }
        public List<Invoice> Invoices { get; set; }
        public ObservableCollection<Payment> Payments { get; private set; }
        public object CollectionLock { get; private set; }

        public bool SheetDataReady
        {
            get { return !Invoices.IsNullOrEmpty() && !Payments.IsNullOrEmpty(); }
        }

        public IdentifierFacade()
        {
            Payments = new ObservableCollection<Payment>();
            CollectionLock = new object();
            CustomerDatabase = new CustomerDatabaseCreatorTextConfig().CustomerDatabase; //new CustomerDatabaseCreatorApplicationConfig().CustomerDatabase;
            BindingOperations.EnableCollectionSynchronization(Payments, CollectionLock);
        }
    }
}
