/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.ViewModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;

namespace PaymentsIdentifier.View
{
    /// <summary>
    /// Interaction logic for CustomerDatabaseEditor.xaml
    /// </summary>
    [Export]
    internal partial class CustomerDatabaseWindow : Window
    {
        public CustomerDatabaseWindow()
        {
            InitializeComponent();
        }

        [Import]
        CustomerDatabaseViewModel ViewModel
        {
            get { return DataContext as CustomerDatabaseViewModel; }
            set { DataContext = value; }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}
