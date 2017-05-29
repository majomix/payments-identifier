/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using System.Windows;

namespace PaymentsIdentifier.View
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    internal partial class NotificationWindow : Window
    {
        public NotificationWindow()
        {
            InitializeComponent();
        }

        public DataTemplate NotificationTemplate
        {
            get { return (DataTemplate)GetValue(NotificationTemplateProperty); }
            set { SetValue(NotificationTemplateProperty, value); }
        }

        public static readonly DependencyProperty NotificationTemplateProperty =
            DependencyProperty.Register(
                "NotificationTemplate",
                typeof(DataTemplate),
                typeof(NotificationWindow),
                new PropertyMetadata(null));

    }
}
