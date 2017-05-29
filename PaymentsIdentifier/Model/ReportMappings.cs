/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using System.Collections.Generic;
using System.Configuration;

namespace PaymentsIdentifier.Model
{
    internal static class ReportMappings
    {
        // Unallocated Report Mappings
        public const string byOrderOfBeneficiaryMapping = @"By Order Of / Beneficiary";
        public const string unallocatedMapping = "UNALLOCATED";
        public const string documentNumberMapping = "Document Number";
        public const string paymentDetailsMapping = "Payment Details ";
        public const string valueDateMapping = "Value Date";

        // Daily Report Mappings
        public const string agingMapping = "EMEA AGE";
        public const string countryMapping = "COUNTRY";
        public const string customerIdMapping = "CUSTOMER ID";
        public const string customerNameMapping = "CUSTOMER NAME";
        public const string invoiceReferenceMapping = "INVOICE REFERENCE";
        public const string valueMapping = "AMOUNT OUTSTANDING (DOCUMENT CURRENCY)";

        public const string dailyReportPath = @"F:\C#\PaymentsIdentifier\xls\EMEA_Daily_Open_AR.xlsx";
        public const string unallocatedReportPath = @"F:\C#\PaymentsIdentifier\xls\North160118.xlsx";
        public const string testFilePath = @"F:\C#\PaymentsIdentifier\xls\abc.txt";

        public const string defaultRegion = "North";

        public const int maximumNumberOfInvoices = 26;
        
        //public static string[] validSheetNames = new string[] { "Norway", "Sweden", "Denmark", "Finland", "Belgium", "Netherlands" };
        public static string[] validSheetNames = ConfigurationManager.AppSettings["validSheetNames"].Split(',');

        public static IEnumerable<string> RequiredDailyReportMappings()
        {
            yield return valueMapping;
            yield return countryMapping;
            yield return customerIdMapping;
        }

        public static IEnumerable<Country> SupportedCountries()
        {
            foreach(string country in ConfigurationManager.AppSettings["validSheetNames"].Split(','))
            {
                string[] tokens = country.Split(';');
                yield return new Country(tokens[0], ReportMappings.defaultRegion, tokens[1]);
            }
        }
    }
}
