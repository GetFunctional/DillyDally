using System;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;

namespace GF.DillyDally.Wpf.Client.Core.Commands
{
    internal sealed class CommandFactory
    {
        public ICommand CreateFromTask(
            Func<Task> execute,
            Func<bool> canExecute = null)
        {
            var command = new AsyncCommand(execute, canExecute);
            return command;
        }

        internal ICommand<TParam> CreateFromTask<TParam>(
            Func<TParam, Task> execute,
            Func<TParam, bool> canExecute = null)
        {
            var command = new AsyncCommand<TParam>(execute, canExecute);
            return command;
        }

        public ICommand CreateFromAction(Action execute,
            Func<bool> canExecute = null)
        {
            return new DelegateCommand(execute, canExecute);
        }
    }
}