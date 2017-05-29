/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using System;
using System.Threading;
using System.Windows.Input;

namespace PaymentsIdentifier.Commands
{
    public sealed class CancelAsynchronousCommand : ICommand
    {
        private CancellationTokenSource myCancellationTokenSource = new CancellationTokenSource();
        private bool myCommandExecuting;

        public CancellationToken Token
        {
            get { return myCancellationTokenSource.Token; }
        }

        public void NotifyCommandStarting()
        {
            myCommandExecuting = true;
            if (!myCancellationTokenSource.IsCancellationRequested) return;
            myCancellationTokenSource = new CancellationTokenSource();
            RaiseCanExecuteChanged();
        }

        public void NotifyCommandFinished()
        {
            myCommandExecuting = false;
            RaiseCanExecuteChanged();
        }

        public bool CanExecute(object parameter)
        {
            return myCommandExecuting && !myCancellationTokenSource.IsCancellationRequested;
        }

        public void Execute(object parameter)
        {
            myCancellationTokenSource.Cancel();
            RaiseCanExecuteChanged();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
