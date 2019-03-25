using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace GF.DillyDally.Wpf.Client.Core.Dialoge
{
    internal sealed class DialogSettings : IDialogSettings
    {
        private static readonly Size DefaultInitialSize = new Size(double.NaN, double.NaN);

        public DialogSettings(IDialogResult defaultDialogResult, IList<IDialogResult> availableDialogResults) : this(
            defaultDialogResult, availableDialogResults, DefaultInitialSize)
        {
        }

        public DialogSettings(IDialogResult defaultDialogResult, IList<IDialogResult> availableDialogResults,
            Size initialDialogSize)
        {
            this.AvailableDialogResults = new List<IDialogResult>(availableDialogResults);
            this.DefaultDialogResult = defaultDialogResult ?? availableDialogResults.First();
            this.InitialDialogSize = initialDialogSize;
        }

        #region IDialogSettings Members

        public IDialogResult DefaultDialogResult { get; }
        public Size InitialDialogSize { get; }
        public IReadOnlyList<IDialogResult> AvailableDialogResults { get; }

        #endregion
    }
}