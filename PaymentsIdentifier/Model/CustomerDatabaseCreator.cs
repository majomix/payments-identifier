/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace PaymentsIdentifier.Model
{
    internal class CustomerDatabaseCreator
    {
        public List<Customer> CustomerDatabaseFactoryMethod(bool fromConfig)
        {
            List<Customer> customerDatabase = new List<Customer>();

            if(fromConfig)
            {
                PopulateFromConfig(customerDatabase);
            }
            else
            {
                PopulateFromDefault(customerDatabase);
            }

            return customerDatabase;
        }

        private void PopulateFromConfig(List<Customer> customerDatabase)
        {
            foreach (string customer in ConfigurationManager.AppSettings["databaseEntries"].Split('*'))
            {
                string[] tokens = customer.Split(';');
                List<string> listOfIDs = new List<string>();

                foreach (string customerID in tokens[2].Split(','))
                {
                    listOfIDs.Add(customerID);
                }

                Country country = ReportMappings.SupportedCountries().Where(_ => _.Name == tokens[0]).SingleOrDefault();

                if (country != null) customerDatabase.Add(new Customer(country, tokens[1], listOfIDs));
            }
        }

        private void PopulateFromDefault(List<Customer> customerDatabase)
        {
            Country country = ReportMappings.SupportedCountries().Where(_ => _.Name == "Netherlands").SingleOrDefault();

            customerDatabase.Add(new Customer(country, "1/ARROW CENTRAL EUROPE", new List<string>() { "401331" }));
            customerDatabase.Add(new Customer(country, "VODAFONE PROCUREMENT COMPANY SARL", new List<string>() { "1213541304" }));
            customerDatabase.Add(new Customer(country, "PLIEGER B.V.", new List<string>() { "401561" }));
            customerDatabase.Add(new Customer(country, "ORACLE NEDERLAND BV", new List<string>() { "1213105725" }));
            customerDatabase.Add(new Customer(country, "CE - IT B.V.", new List<string>() { "400386" }));
            customerDatabase.Add(new Customer(country, "UPS SCS (NEDERLAND) B.V.", new List<string>() { "1213259631" }));
            customerDatabase.Add(new Customer(country, "VALEO SERVICE BENELUX B.V.", new List<string>() { "1213105834" }));
            customerDatabase.Add(new Customer(country, "Straumann BV", new List<string>() { "400499" }));

            country = ReportMappings.SupportedCountries().Where(_ => _.Name == "Belgium").SingleOrDefault();
            customerDatabase.Add(new Customer(country, "GDF SUEZ TRADING BRUSSELS", new List<string>() { "1214327535" }));
            customerDatabase.Add(new Customer(country, "ORACLE BELGIUM PAYME", new List<string>() { "1213077931" }));
            customerDatabase.Add(new Customer(country, "UPS EUROPE SPRL", new List<string>() { "1213664802" }));
            customerDatabase.Add(new Customer(country, "BASE Company n.v./s.a.", new List<string>() { "99979" }));
            customerDatabase.Add(new Customer(country, "Pfizer Manufacturing Belg", new List<string>() { "1213664655" }));
            customerDatabase.Add(new Customer(country, "SKYPE COMMUNICATIONS SARL", new List<string>() { "1214234003" }));
            customerDatabase.Add(new Customer(country, "GTECH GLOBAL SERVICES CORPORATION", new List<string>() { "101008", "100350", "100651" }));
            customerDatabase.Add(new Customer(country, "UNITED PARCEL SERVICE BELG", new List<string>() { "1213260301" }));
        }
    }
}
