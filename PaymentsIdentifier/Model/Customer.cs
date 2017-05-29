/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PaymentsIdentifier.Model
{
    internal class Customer : BindableBase
    {
        private Country myCountry;
        private string myCollector;
        private ObservableCollection<string> myIdentifiers;
        private string myName;

        public Customer(Country country, string name, IEnumerable<string> identifiers)
        {
            Country = country;
            Name = name;
            Identifiers = new ObservableCollection<string>(identifiers);
        }

        public Customer(Country country, string name)
            : this(country, name, Enumerable.Empty<string>()) { }

        public Country Country
        {
            get { return myCountry; }
            set { SetProperty(ref myCountry, value); }
        }
        public string Collector
        {
            get { return myCollector; }
            set { SetProperty(ref myCollector, value); }
        }
        public ObservableCollection<string> Identifiers
        {
            get { return myIdentifiers; }
            private set { SetProperty(ref myIdentifiers, value); }
        }
        public string Name
        {
            get { return myName; }
            set { SetProperty(ref myName, value); }
        }

        public Customer CloneCustomer()
        {
            ObservableCollection<string> newIdentifiers = new ObservableCollection<string>();

            foreach (string id in Identifiers)
            {
                newIdentifiers.Add(id);
            }
            return new Customer(Country, Name, newIdentifiers);
        }

        internal void Clear()
        {
            Collector = string.Empty;
            Identifiers.Clear();
            Name = string.Empty;
        }
    }
}
