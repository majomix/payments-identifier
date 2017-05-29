/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using Prism.Interactivity.InteractionRequest;
using System;
using System.Windows;

namespace PaymentsIdentifier.View
{
    internal class PopupWindowAction : PopupWindowActionBase
    {
        /// <summary>
        /// Gets/sets the <see cref="DataTemplate"/> representing the content of popup window
        /// </summary>
        public static readonly DependencyProperty ContentTemplateProperty =
            DependencyProperty.Register(
                "ContentTemplate",
                typeof(DataTemplate),
                typeof(PopupWindowAction),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets/sets the type of custom popup window
        /// </summary>
        public static readonly DependencyProperty PopupWindowTypeProperty =
            DependencyProperty.Register(
                "PopupWindowType",
                typeof(Type),
                typeof(PopupWindowAction),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets/sets the type of custom popup window
        /// </summary>
        public Type PopupWindowType
        {
            get { return (Type)GetValue(PopupWindowTypeProperty); }
            set { SetValue(PopupWindowTypeProperty, value); }
        }

        /// <summary>
        /// Gets/sets the <see cref="DataTemplate"/> representing the content of popup window
        /// </summary>
        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        protected override Window GetPopupWindow(INotification notification)
        {
            Window popupWindow = (PopupWindowType != null)
                ? (Window)PopupWindowType.GetConstructor(new Type[] { }).Invoke(new object[] { })
                : CreateDefaultWindow(notification);
            popupWindow.DataContext = notification;

            return popupWindow;
        }

        private Window CreateDefaultWindow(INotification notification)
        {
            IsModal = true;
            return notification is Confirmation ? new ConfirmationWindow() { ConfirmationTemplate = ContentTemplate }
                : (Window)new NotificationWindow() { NotificationTemplate = ContentTemplate };
        }
    }
}