/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.Commands;
using PaymentsIdentifier.Model;
using Prism.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace PaymentsIdentifier.ViewModel
{
    [Export]
    internal class CustomerDatabaseViewModel
    {
        private IEventAggregator myEventAggregator;
        private IdentifierFacade myFacade;

        [ImportingConstructor]
        public CustomerDatabaseViewModel(IEventAggregator eventAggregator, IdentifierFacade facade)
        {
            myEventAggregator = eventAggregator;
            myFacade = facade;

            NewCustomer = new Customer(null, "");
        }

        [Import(typeof(AddCustomerCommand))]
        public ICommand AddCustomerCommand { get; private set; }

        [Import(typeof(SaveCustomerDatabaseCommand))]
        public ICommand SaveCustomerDatabaseCommand { get; private set; }

        public ObservableCollection<Customer> Customers
        {
            get { return myFacade.CustomerDatabase; }
        }

        public IEnumerable<Country> AvailableCountries
        {
            get { return ReportMappings.SupportedCountries(); }
        }

        public Customer NewCustomer { get; private set; }



    }
}
