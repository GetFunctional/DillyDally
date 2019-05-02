using System;
using DevExpress.Mvvm;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public sealed class AddTaskCommand : DelegateCommand<string>
    {
        public AddTaskCommand(Action<string> executeMethod) : base(executeMethod)
        {
        }

        public override bool CanExecute(string parameter)
        {
            return !string.IsNullOrWhiteSpace(parameter);
        }
    }
}