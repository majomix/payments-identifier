/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.ViewModel;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace PaymentsIdentifier.Commands
{
    [Export]
    internal class ObsoleteLoadExcelFilesCommand : ICommand
    {
        public BackgroundWorker Worker { get; private set; }

        public ObsoleteLoadExcelFilesCommand()
        {
            Worker = new BackgroundWorker();
            Worker.DoWork += DoWork;
        }

        public bool CanExecute(object parameter)
        {
            var p = parameter as MainWindowViewModel;
            return p != null && p.DailyReportFilePath != null && p.UnallocatedCashReportFilePath != null;
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
            //mainViewModel.LoadExcelFiles();
        }
    }
}
