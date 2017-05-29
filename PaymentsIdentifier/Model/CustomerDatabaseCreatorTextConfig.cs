/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PaymentsIdentifier.Model
{
    internal class CustomerDatabaseCreatorTextConfig : CustomerDatabaseCreatorAbstract
    {
        public CustomerDatabaseCreatorTextConfig() : base()
        {
            using (FileStream fileStream = File.Open(GetAssemblyDirectory() + @"\customers.ini", FileMode.Open))
            {
                StreamReader reader = new StreamReader(fileStream, Encoding.Unicode);
                string line = null;
                Country country = null;
                string customerName = null;
                IEnumerable<string> customerIds = null;

                while ((line = reader.ReadLine()) != null)
                {
                    // new section
                    if(line.StartsWith("[") && line.EndsWith("]"))
                    {
                        string nameToken = line.Split('[', ']')[1];
                        country = ReportMappings.SupportedCountries().Where(_ => _.Name == nameToken).SingleOrDefault();
                        if(country == null) country = new Country(nameToken, ReportMappings.defaultRegion, nameToken);
                    }
                    else
                    {
                        string[] tokens = line.Split(new Char[] {'='}, 2);
                        string entryName = tokens[0];
                        string entryValue = tokens[1];

                        if(entryName == "Name")
                        {
                            customerName = entryValue;
                        }
                        else if(entryName == "Id")
                        {
                            customerIds = entryValue.Split(',');

                            if (!customerName.IsNullOrEmpty() && !customerIds.IsNullOrEmpty())
                            {
                                CustomerDatabase.Add(new Customer(country, customerName, customerIds));
                            }

                            customerName = null;
                            customerIds = null;
                        }
                    }
                }
            }
        }

        private static string GetAssemblyDirectory()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            UriBuilder uri = new UriBuilder(dir);
            string path = Uri.UnescapeDataString(uri.Path);

            return Path.GetDirectoryName(path);
        }
    }
}
