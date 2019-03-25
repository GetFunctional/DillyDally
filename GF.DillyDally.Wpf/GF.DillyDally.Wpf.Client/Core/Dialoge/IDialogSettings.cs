using System.Collections.Generic;
using System.Windows;

namespace GF.DillyDally.Wpf.Client.Core.Dialoge
{
    public interface IDialogSettings
    {
        IReadOnlyList<IDialogResult> AvailableDialogResults { get; }

        IDialogResult DefaultDialogResult { get; }

        Size InitialDialogSize { get; }
    }
}