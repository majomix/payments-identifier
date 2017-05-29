/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.Events;
using PaymentsIdentifier.Model;
using PaymentsIdentifier.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;

namespace PaymentsIdentifier.ViewModel
{
    [Export]
    internal class EditAllocationsWindowViewModel : BindableBase
    {
        private IdentifierFacade myFacade;
        private IEventAggregator myEventAggregator;

        [ImportingConstructor]
        public EditAllocationsWindowViewModel(IEventAggregator eventAggregator, IdentifierFacade facade)
        {
            myFacade = facade;
            myEventAggregator = eventAggregator;

            myEventAggregator.GetEvent<InvoicesMatchedEvent>().Subscribe(
                (input) =>
                {
                    OnPropertyChanged("Payments");
                    OnPropertyChanged("TotalPayments");
                    OnPropertyChanged("FilteredPayments");
                    OnPropertyChanged("MatchedPayments");
                    OnPropertyChanged("PaymentsWithoutComments");
                });
        }

        [Import(typeof(InsertInputCommand))]
        public ICommand InsertInputCommand { get; private set; }


        [Import(typeof(UpdateInvoiceListCommand))]
        public ICommand UpdateInvoiceListCommand { get; private set; }

        public ObservableCollection<Payment> Payments
        {
            get { return myFacade.Payments; }
        }

        public int TotalPayments
        {
            get { return myFacade.Payments.Count; }
        }

        public int FilteredPayments
        {
            get { return myFacade.Payments.Count; }
        }
        public int MatchedPayments
        {
            get { return myFacade.Payments.Count(_ => _.MatchedInvoices.Count > 0); }
        }
        public int PaymentsWithoutComments
        {
            get { return myFacade.Payments.Count(_ => _.Commentary.IsNullOrEmpty()); }
        }
    }
}
