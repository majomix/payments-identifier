/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.Events;
using PaymentsIdentifier.ViewModel;
using Prism.Events;
using System.ComponentModel.Composition;
using System.Windows;

namespace PaymentsIdentifier.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    [Export]
    internal partial class MainWindow : Window
    {
        private IEventAggregator myEventAggregator;
        private EditAllocationsWindow myEditAllocationsWindow;
        private CustomerDatabaseWindow myCustomerDatabaseWindow;

        [ImportingConstructor]
        public MainWindow(IEventAggregator eventAggregator, EditAllocationsWindow editAllocationsWindow, CustomerDatabaseWindow customerDatabaseWindow)
        {
            myEditAllocationsWindow = editAllocationsWindow;
            myCustomerDatabaseWindow = customerDatabaseWindow;
            myEventAggregator = eventAggregator;
            myEventAggregator.GetEvent<InvoicesMatchedEvent>().Subscribe(_ => myEditAllocationsWindow.ShowDialog(), ThreadOption.UIThread);
            InitializeComponent();

            this.Closed += (sender, e) => this.Dispatcher.InvokeShutdown();
        }

        [Import]
        MainWindowViewModel ViewModel
        {
            get { return DataContext as MainWindowViewModel; }
            set { DataContext = value; }
        }

        private void ShowCustomerDatabase(object sender, RoutedEventArgs e)
        {
            myCustomerDatabaseWindow.ShowDialog();
        }
    }
}
