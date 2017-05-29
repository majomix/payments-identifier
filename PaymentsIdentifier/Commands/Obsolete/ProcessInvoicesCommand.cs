/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.ViewModel;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PaymentsIdentifier.Commands
{
    [Export]
    internal class ProcessInvoicesCommand : ICommand
    {
        public BackgroundWorker Worker { get; private set; }

        public ProcessInvoicesCommand()
        {
            Worker = new BackgroundWorker();
            Worker.DoWork += DoWork;
        }

        public bool CanExecute(object parameter)
        {
            var p = parameter as MainWindowViewModel;
            return p != null && p.OutputFilePath != null;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            MainWindowViewModel mainViewModel = (MainWindowViewModel)parameter;
            Worker.RunWorkerAsync(mainViewModel);
        }

        protected void DoWork(object sender, DoWorkEventArgs e)
        {
            MainWindowViewModel mainViewModel = (MainWindowViewModel)e.Argument;
            //mainViewModel.ProcessInvoices();
        }

        async Task<int> DoWork2(CancellationToken cancellationToken, IProgress<string> progress)
        {
            return await Task.Factory.StartNew(() => progress.Report("First"), cancellationToken)
                             .ContinueWith(_ => Thread.Sleep(1000), cancellationToken)
                             .ContinueWith(_ => progress.Report("Second"), cancellationToken)
                             .ContinueWith(_ => Thread.Sleep(1000), cancellationToken)
                             .ContinueWith(_ => 42);
        }
    }
}
