/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.Events;
using PaymentsIdentifier.Model;
using PaymentsIdentifier.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Prism.Mvvm;

namespace PaymentsIdentifier.ViewModel
{
    [Export]
    internal class InsertInputWindowViewModel : BindableBase
    {
        private IEventAggregator myEventAggregator;
        private IdentifierFacade myFacade;
        private Payment mySelectedPayment;
        private ObservableCollection<Invoice> mySelectedMatchedList;

        [Import(typeof(UpdateInvoiceListCommand))]
        public ICommand UpdateInvoiceListCommand { get; set; }

        [Import(typeof(SelectAdjacentPaymentCommand))]
        public ICommand SelectAdjacentPaymentCommand { get; set; }

        [Import(typeof(SelectAdjacentMatchedListCommand))]
        public ICommand SelectAdjacentMatchedListCommand { get; set; }

        [Import(typeof(SetAsMatchedCommand))]
        public ICommand SetAsMatchedCommand { get; set; }

        [Import(typeof(InsertInvoicesCommand))]
        public IInteractionCommand InsertInvoicesCommand { get; set; }

        [ImportingConstructor]
        public InsertInputWindowViewModel(IEventAggregator eventAggregator, IdentifierFacade facade)
        {
            myEventAggregator = eventAggregator;
            myFacade = facade;

            myEventAggregator.GetEvent<ShowInsertInputEvent>().Subscribe(payment =>
                {
                    SelectedPayment = payment;
                });
            myEventAggregator.GetEvent<SelectedPaymentChangedEvent>().Subscribe(payment =>
                {
                    SelectedPayment = payment;
                });
            myEventAggregator.GetEvent<SelectedMatchedListChangedEvent>().Subscribe(invoices =>
                {
                    SelectedMatchedList = invoices;
                });
        }

        public Payment SelectedPayment
        {
            get { return mySelectedPayment; }
            set
            {
                if(mySelectedPayment != value)
                {
                    mySelectedPayment = value;
                    SelectedMatchedList = mySelectedPayment.FinalMatchedInvoices ?? mySelectedPayment.MatchedInvoices.FirstOrDefault();
                    OnPropertyChanged("SelectedPayment");
                }
            } 
        }

        public ObservableCollection<Invoice> SelectedMatchedList
        {
            get { return mySelectedMatchedList; }
            set
            {
                if (mySelectedMatchedList != value)
                {
                    mySelectedMatchedList = value;
                    OnPropertyChanged("SelectedMatchedList");
                }
                OnPropertyChanged("SelectedMatchedListIndex");
                OnPropertyChanged("NumberOfInvoices");
                OnPropertyChanged("ValueOfInvoices");
                OnPropertyChanged("Remainder");
            }
        }

        public int SelectedMatchedListIndex
        {
            get
            {
                if (SelectedPayment == null || SelectedMatchedList == null) return 0;
                return SelectedPayment.MatchedInvoices.IndexOf(SelectedMatchedList) + 1;
            }
        }

        public int NumberOfInvoices
        {
            get
            {
                if (SelectedMatchedList != null)
                {
                    return SelectedMatchedList.Select(_ => _.Value).Distinct().Count();
                }
                else return 0;
            }
        }

        public double ValueOfInvoices
        {
            get
            {
                if (SelectedMatchedList != null)
                {
                    return SelectedMatchedList.Select(_ => _.Value).Distinct().Sum(); 
                }
                else return 0;
            }
        }

        public string Remainder
        {
            get
            {
                if (SelectedPayment != null && ValueOfInvoices != 0)
                {
                    return (SelectedPayment.Value - ValueOfInvoices).ToString();
                }
                return "-";
            }
        }

        public ObservableCollection<Payment> AllPayments
        {
            get { return myFacade.Payments; }
        }

        private bool myIncludeResolved;
        public bool IncludeResolved
        {
            get { return myIncludeResolved; }
            set
            {
                if (value != myIncludeResolved)
                {
                    myIncludeResolved = value;
                    OnPropertyChanged("IncludeResolved");
                }
            }
        }
    }
}
