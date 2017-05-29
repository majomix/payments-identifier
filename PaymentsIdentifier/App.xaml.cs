/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using System.Windows;
using System.Windows.Threading;

namespace PaymentsIdentifier
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DispatcherUnhandledException += DispatcherUnhandledExceptionHandler;

            base.OnStartup(e);
            new Bootstrapper().Run();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            //Settings.Default.Save();
            base.OnExit(e);
        }

        private void DispatcherUnhandledExceptionHandler(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            //new UnhandledExceptionDialog() { DataContext = new UnhandledExceptionViewModel(e.Exception) }.ShowDialog();
            e.Handled = true;
            App.Current.Shutdown();
        }
    }
}
