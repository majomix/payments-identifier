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
    /// Interaction logic for InsertInputWindow.xaml
    /// </summary>
    [Export]
    internal partial class InsertInputWindow : Window
    {
        public InsertInputWindow()
        {
            InitializeComponent();
        }

        [Import]
        InsertInputWindowViewModel ViewModel
        {
            get { return DataContext as InsertInputWindowViewModel; }
            set { DataContext = value; }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}
