using System;
using System.Threading.Tasks;
using DevExpress.Mvvm;

namespace GF.DillyDally.Wpf.Client.Presentation.Common.Currencies
{
    public sealed class AddNewCurrencyCommand : AsyncCommand
    {
        public AddNewCurrencyCommand(Func<Task> executeMethod) : base(executeMethod)
        {
        }
    }
}