/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using System.Collections.Generic;
using System.Linq;

namespace PaymentsIdentifier.Model
{
    internal class CustomerDatabaseCreatorDefault : CustomerDatabaseCreatorAbstract
    {
        public CustomerDatabaseCreatorDefault() : base()
        {
            Country country = ReportMappings.SupportedCountries().Where(_ => _.Name == "Netherlands").SingleOrDefault();

            CustomerDatabase.Add(new Customer(country, "1/ARROW CENTRAL EUROPE", new List<string>() { "401331" }));
            CustomerDatabase.Add(new Customer(country, "VODAFONE PROCUREMENT COMPANY SARL", new List<string>() { "1213541304" }));
            CustomerDatabase.Add(new Customer(country, "PLIEGER B.V.", new List<string>() { "401561" }));
            CustomerDatabase.Add(new Customer(country, "ORACLE NEDERLAND BV", new List<string>() { "1213105725" }));
            CustomerDatabase.Add(new Customer(country, "CE - IT B.V.", new List<string>() { "400386" }));
            CustomerDatabase.Add(new Customer(country, "UPS SCS (NEDERLAND) B.V.", new List<string>() { "1213259631" }));
            CustomerDatabase.Add(new Customer(country, "VALEO SERVICE BENELUX B.V.", new List<string>() { "1213105834" }));
            CustomerDatabase.Add(new Customer(country, "Straumann BV", new List<string>() { "400499" }));

            country = ReportMappings.SupportedCountries().Where(_ => _.Name == "Belgium").SingleOrDefault();
            CustomerDatabase.Add(new Customer(country, "GDF SUEZ TRADING BRUSSELS", new List<string>() { "1214327535" }));
            CustomerDatabase.Add(new Customer(country, "ORACLE BELGIUM PAYME", new List<string>() { "1213077931" }));
            CustomerDatabase.Add(new Customer(country, "UPS EUROPE SPRL", new List<string>() { "1213664802" }));
            CustomerDatabase.Add(new Customer(country, "BASE Company n.v./s.a.", new List<string>() { "99979" }));
            CustomerDatabase.Add(new Customer(country, "Pfizer Manufacturing Belg", new List<string>() { "1213664655" }));
            CustomerDatabase.Add(new Customer(country, "SKYPE COMMUNICATIONS SARL", new List<string>() { "1214234003" }));
            CustomerDatabase.Add(new Customer(country, "GTECH GLOBAL SERVICES CORPORATION", new List<string>() { "101008", "100350", "100651" }));
            CustomerDatabase.Add(new Customer(country, "UNITED PARCEL SERVICE BELG", new List<string>() { "1213260301" }));
        }
    }
}
