/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using System.Collections.Generic;
using System.Linq;

namespace PaymentsIdentifier.Model
{
    internal class InvoiceFilterChain
    {
        private double myAging;
        private double myValue;

        public InvoiceFilterChain(double aging, double value)
        {
            myAging = aging;
            myValue = value;
        }

        public List<Invoice> FilterInvoices(IEnumerable<Invoice> relevantInvoices)
        {
            double sumOfNegativeInvoices = relevantInvoices.Where(_ => _.Value < 0).Sum(_ => _.Value);
            List<Invoice> filteredInvoices = relevantInvoices.GroupBy(_ => _.Value).Where(_ => _.Key + sumOfNegativeInvoices <= myValue).Select(_ => _.OrderBy(g => g.Aging).First()).ToList();

            if (filteredInvoices.Count >= ReportMappings.maximumNumberOfInvoices)
            {
                filteredInvoices = filteredInvoices.Where(_ => _.Aging >= myAging).ToList();

                if (filteredInvoices.Count >= ReportMappings.maximumNumberOfInvoices)
                {
                    filteredInvoices = filteredInvoices.OrderByDescending(_ => _.Aging).Take(ReportMappings.maximumNumberOfInvoices).ToList();
                }
            }

            return filteredInvoices;
        }
    }
}
