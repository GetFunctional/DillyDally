using System;
using System.Threading.Tasks;
using DevExpress.Mvvm;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public sealed class AddTaskCommand : AsyncCommand<string>
    {
        public AddTaskCommand(Func<string, Task> executeMethod) : base(executeMethod)
        {
        }

        public override bool CanExecute(string parameter)
        {
            return !string.IsNullOrWhiteSpace(parameter);
        }
    }
}