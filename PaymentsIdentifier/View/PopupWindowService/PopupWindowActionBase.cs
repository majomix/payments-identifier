/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using Prism.Interactivity.InteractionRequest;
using System;
using System.Windows;
using System.Windows.Interactivity;

namespace PaymentsIdentifier.View
{
    internal abstract class PopupWindowActionBase : TriggerAction<FrameworkElement>
    {
        /// <summary>
        /// Gets/sets flag whether the popup window is displayed as modal
        /// </summary>
        public static readonly DependencyProperty IsModalProperty =
            DependencyProperty.Register(
                "IsModal",
                typeof(bool),
                typeof(PopupWindowActionBase),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets/sets flag whether the popup window is displayed as modal
        /// </summary>
        public bool IsModal
        {
            get { return (bool)GetValue(IsModalProperty); }
            set { SetValue(IsModalProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            InteractionRequestedEventArgs args = parameter as InteractionRequestedEventArgs;
            if (args == null)
            {
                return;
            }

            Window popupWindow = GetPopupWindow(args.Context);

            EventHandler handler = null;
            handler =
                (o, e) =>
                {
                    popupWindow.Closed -= handler;
                    args.Callback();
                };
            popupWindow.Closed += handler;

            if (IsModal)
            {
                popupWindow.ShowDialog();
            }
            else
            {
                popupWindow.Show();
            }
        }

        /// <summary>
        /// Gets the popup window to be shown
        /// </summary>
        /// <param name="notification">Interaction request used for notification</param>
        /// <returns>Popup window to be shown</returns>
        protected abstract Window GetPopupWindow(INotification notification);
    }
}
