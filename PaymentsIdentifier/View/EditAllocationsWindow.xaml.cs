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
    /// Interaction logic for EditAllocationsWindows.xaml
    /// </summary>
    ///
    [Export]
    internal partial class EditAllocationsWindow : Window
    {
        private IEventAggregator myEventAggregator;
        private InsertInputWindow myInsertInputWindow;

        [ImportingConstructor]
        public EditAllocationsWindow(IEventAggregator eventAggregator, InsertInputWindow insertInputWindow)
        {
            myEventAggregator = eventAggregator;
            myInsertInputWindow = insertInputWindow;

            InitializeComponent();

            myEventAggregator.GetEvent<ShowInsertInputEvent>().Subscribe(_ => myInsertInputWindow.ShowDialog(), ThreadOption.UIThread);
            myEventAggregator.GetEvent<InvoicesMatchedEvent>().Subscribe(_ =>
            {
                paymentsDataGrid.Items.Refresh();
            });
            myEventAggregator.GetEvent<InvoiceDetailsChangedEvent>().Subscribe(_ =>
            {
                paymentsDataGrid.Items.Refresh();
            });
        }

        [Import]
        EditAllocationsWindowViewModel ViewModel
        {
            get { return DataContext as EditAllocationsWindowViewModel; }
            set { DataContext = value; }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}
