/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.ViewModel;
using System;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace PaymentsIdentifier.Commands
{
    [Export]
    internal class LoadFileNameCommand : ICommand
    {
        private IFilePathProvider myFilePathProvider;

        [ImportingConstructor]
        public LoadFileNameCommand(IFilePathProvider filePathProvider)
        {
            myFilePathProvider = filePathProvider;
        }

        public bool CanExecute(object parameter)
        {
            var p = parameter as object[];
            return (p != null && p.Length == 2 && p[0] is MainWindowViewModel && p[1] is int);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            object[] parameters = (object[])parameter;
            MainWindowViewModel mainViewModel = (MainWindowViewModel)parameters[0];
            int type = (int)parameters[1];

            string filePath = (type < 2) ? myFilePathProvider.GetOpenFilePath() : myFilePathProvider.GetSaveFilePath();
            if (!String.IsNullOrWhiteSpace(filePath))
            {
                switch(type)
                {
                    case 0:
                        mainViewModel.DailyReportFilePath = filePath;
                        break;
                    case 1:
                        mainViewModel.UnallocatedCashReportFilePath = filePath;
                        break;
                    case 2:
                        mainViewModel.OutputFilePath = filePath;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }
    }

}
