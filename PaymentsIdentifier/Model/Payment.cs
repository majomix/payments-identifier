/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.ViewModel;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PaymentsIdentifier.Model
{
    internal class Payment : BindableBase
    {
        private ObservableCollection<Invoice> myFinalMatchedInvoices;

        public Payment()
        {
            MatchedInvoices = new ObservableCollection<ObservableCollection<Invoice>>();
        }

        public double BankFees { get; set; }
        public string Commentary { get; set; }
        public string Country { get; set; }
        public string CustomerName { get; set; }
        public string DocumentNumber { get; set; }
        public ObservableCollection<Invoice> FinalMatchedInvoices
        {
            get { return myFinalMatchedInvoices; }
            set
            {
                if(myFinalMatchedInvoices != value)
                {
                    myFinalMatchedInvoices = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<Invoice> Invoices { get; set; }
        public ObservableCollection<ObservableCollection<Invoice>> MatchedInvoices { get; private set; }
        public string PaymentDate { get; set; }
        public string PaymentDetails { get; set; }
        public int PaymentOrdinalNumber { get; set; }
        public double PennyElimination { get; set; }
        public PaymentType Type { get; set; }
        public IdentifyStatus Status { get; set; }
        public double Value { get; set; }

        public void PairMatchedInvoiceValues(List<List<double>> input)
        {
            if (input.IsNullOrEmpty()) return;

            foreach(List<double> list in input)
            {
                MatchedInvoices.Add(new ObservableCollection<Invoice>(Invoices.Where(_ => list.Contains(_.Value))));
            }

            if(MatchedInvoices.Count == 1 && MatchedInvoices.First().Select(_ => _.Value).Sum() == Value)
            {
                FinalMatchedInvoices = MatchedInvoices.First();
                Status = IdentifyStatus.IdentifiedByMatching;
            }
            else if (MatchedInvoices.Count == 1 && MatchedInvoices.First().Select(_ => _.Value).Distinct().Sum() == Value)
            {
                Status = IdentifyStatus.IdentifiedByMatchingWithMultipleSameValues;
            }
        }

        public List<string> PairExtractedInvoiceNumbers(List<string> input)
        {
            List<Invoice> pairedInvoices = Invoices.Where(_ => input.Contains(_.InvoiceNumber)).ToList();
            
            if(!pairedInvoices.IsNullOrEmpty())
            {
                MatchedInvoices.Add(new ObservableCollection<Invoice>(pairedInvoices));

                if (pairedInvoices.Select(_ => _.Value).Sum() == Value)
                {
                    Status = IdentifyStatus.IdentifiedByExtraction;
                    FinalMatchedInvoices = MatchedInvoices.First();
                }
                else
                {
                    Status = IdentifyStatus.PartiallyIdentifiedByExtraction;
                }
            }

            List<string> pairedInvoiceNumbers = pairedInvoices.Select(_ => _.InvoiceNumber).ToList();

            return input.Where(_ => !pairedInvoiceNumbers.Contains(_)).ToList();
        }

        public void AddInvoicesForUnmatchedCustomer(List<Invoice> input)
        {
            MatchedInvoices.Add(new ObservableCollection<Invoice>(input));
            Status = IdentifyStatus.IdentifiedByExtractionWithUnknownCustomer;

            if (MatchedInvoices.First().Select(_ => _.Value).Sum() == Value)
            {
                FinalMatchedInvoices = MatchedInvoices.First();
            }
        }
    }
}
