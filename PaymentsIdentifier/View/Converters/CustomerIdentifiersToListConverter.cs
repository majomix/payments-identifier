/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace PaymentsIdentifier.View
{
    internal class CustomerIdentifiersToListConverter : IMultiValueConverter
    {
        private ObservableCollection<string> myCollection;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            myCollection = (ObservableCollection<string>)values[0];
            return string.Join(", ", myCollection);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string viewList = (string)value;

            myCollection.Clear();
            foreach (string identifier in viewList.Replace(" ", "").Split(','))
            {
                myCollection.Add(identifier);
            }
            return new object[] { myCollection, myCollection.Count };
        }
    }

}
