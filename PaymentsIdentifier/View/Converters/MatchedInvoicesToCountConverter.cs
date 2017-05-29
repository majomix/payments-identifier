/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.Model;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace PaymentsIdentifier.View
{
    internal class MatchedInvoicesToCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<ObservableCollection<Invoice>> matchedInvoices = value as ObservableCollection<ObservableCollection<Invoice>>;
            if (matchedInvoices == null || matchedInvoices.Count == 0) return "0";

            int count = matchedInvoices.Count;
            if (count > 5) return count + " / too many";

            return count + " / " + string.Join(" - ", matchedInvoices.Select(_ => _.Select(list => list.Value).Distinct().Count()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
