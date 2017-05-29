/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using Prism.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PaymentsIdentifier.Commands
{
    internal abstract class AbstractAsynchronousCommand : BindableBase, IAsynchronousCommand
    {
        private NotifyTaskCompletion<int> myExecution;
        protected CancelAsynchronousCommand myCancelCommand;

        public abstract bool CanExecute(object parameter);
        public abstract Task ExecuteAsynchronously(object parameter);

        public async void Execute(object parameter)
        {
            await ExecuteAsynchronously(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        protected void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public NotifyTaskCompletion<int> Execution
        {
            get { return myExecution; }
            protected set
            {
                myExecution = value;
                OnPropertyChanged();
            }
        }
    }
}
