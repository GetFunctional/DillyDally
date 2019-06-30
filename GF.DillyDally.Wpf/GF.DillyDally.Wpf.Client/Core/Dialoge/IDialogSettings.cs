using System.Collections.Generic;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Mvvmc.Contracts;

namespace GF.DillyDally.Wpf.Client.Core.Dialoge
{
    public interface IDialogSettings
    {
        IReadOnlyList<IDialogResult> AvailableDialogResults { get; }

        IDialogResult DefaultDialogResult { get; }
    }
}