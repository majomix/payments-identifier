/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace PaymentsIdentifier.Model
{
    internal class CustomerDatabaseCreatorApplicationConfig : CustomerDatabaseCreatorAbstract
    {
        public CustomerDatabaseCreatorApplicationConfig() : base()
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

                if (country != null) CustomerDatabase.Add(new Customer(country, tokens[1], listOfIDs));
            }
        }

    }
}
