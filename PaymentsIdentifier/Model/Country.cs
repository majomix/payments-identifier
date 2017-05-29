/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

namespace PaymentsIdentifier.Model
{
    internal class Country
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public string SheetName { get; set; }

        public Country(string name, string region, string sheetName)
        {
            Name = name;
            Region = region;
            SheetName = sheetName;
        }
    }
}
