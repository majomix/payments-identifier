/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using System.Windows;

namespace PaymentsIdentifier.View
{
    /// <summary>
    /// Interaction logic for ConfirmationWindow.xaml
    /// </summary>
    internal partial class ConfirmationWindow : Window
    {
        public ConfirmationWindow()
        {
            InitializeComponent();
        }

        public DataTemplate ConfirmationTemplate
        {
            get { return (DataTemplate)GetValue(ConfirmationTemplateProperty); }
            set { SetValue(ConfirmationTemplateProperty, value); }
        }

        public static readonly DependencyProperty ConfirmationTemplateProperty =
            DependencyProperty.Register(
                "ConfirmationTemplate",
                typeof(DataTemplate),
                typeof(ConfirmationWindow),
                new PropertyMetadata(null));
    }
}
