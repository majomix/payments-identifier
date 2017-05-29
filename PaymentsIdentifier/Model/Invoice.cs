/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

namespace PaymentsIdentifier.Model
{
    internal class Invoice
    {
        public int Aging { get; set; }
        public string Country { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public double Value { get; set; }
    }
}
