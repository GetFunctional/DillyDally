using System;
using DevExpress.Mvvm;

namespace GF.DillyDally.Wpf.Client.Presentation.Common.Currencies
{
    public sealed class AddNewCurrencyCommand : DelegateCommand
    {
        public AddNewCurrencyCommand(Action executeMethod) : base(executeMethod)
        {
        }
        //public AddNewCurrencyCommand() : base()
        //{

        //}

        //public override void Execute(object parameter)
        //{
        //    base.Execute(parameter);

        //}

        //public AddNewCurrencyCommand(Func<Task> executeMethod) : base(executeMethod)
        //{
        //}
    }
}