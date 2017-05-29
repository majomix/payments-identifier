/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using LinqToExcel;
using System.Collections.Generic;
using System.Linq;

namespace PaymentsIdentifier.Model
{
    internal class ExcelLoader
    {
        private ExcelQueryFactory dailyReport;
        private ExcelQueryFactory unallocatedReport;

        public ExcelLoader(string dailyReportFilePath, string unallocatedReportFilePath)
        {
            dailyReport = new LinqToExcel.ExcelQueryFactory(dailyReportFilePath);
            dailyReport.AddMapping<Invoice>(_ => _.Aging, ReportMappings.agingMapping);
            dailyReport.AddMapping<Invoice>(_ => _.Country, ReportMappings.countryMapping);
            dailyReport.AddMapping<Invoice>(_ => _.CustomerNumber, ReportMappings.customerIdMapping);
            dailyReport.AddMapping<Invoice>(_ => _.CustomerName, ReportMappings.customerNameMapping);
            dailyReport.AddMapping<Invoice>(_ => _.InvoiceNumber, ReportMappings.invoiceReferenceMapping);
            dailyReport.AddMapping<Invoice>(_ => _.Value, ReportMappings.valueMapping);

            unallocatedReport = new LinqToExcel.ExcelQueryFactory(unallocatedReportFilePath);
            unallocatedReport.AddMapping<Payment>(_ => _.CustomerName, ReportMappings.byOrderOfBeneficiaryMapping);
            unallocatedReport.AddMapping<Payment>(_ => _.PaymentDate, ReportMappings.valueDateMapping);
            unallocatedReport.AddMapping<Payment>(_ => _.DocumentNumber, ReportMappings.documentNumberMapping);
            unallocatedReport.AddMapping<Payment>(_ => _.PaymentDetails, ReportMappings.paymentDetailsMapping);
            unallocatedReport.AddMapping<Payment>(_ => _.Value, ReportMappings.unallocatedMapping);
        }

        public List<Invoice> LoadInvoices()
        {
            return dailyReport.WorksheetRange<Invoice>("A2", "ZZ1048576", "DATA").ToList();
        }

        public List<Payment> LoadPaymentsForSheet(string sheetName)
        {
            return unallocatedReport.WorksheetRange<Payment>("A11", "ZZ1048576", sheetName).Where(_ => _.Value != 0).ToList();
        }

        public IEnumerable<string> GetPaymentsSheetNames()
        {
            return unallocatedReport.GetWorksheetNames();
        }
    }
}
