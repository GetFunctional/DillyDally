using System;

namespace GF.DillyDally.Wpf.Client.Core.Dialoge
{
    public interface IDialogResult
    {
        string DisplayContent { get; }

        Func<bool> DialogConfirmationCondition { get; }
    }
}