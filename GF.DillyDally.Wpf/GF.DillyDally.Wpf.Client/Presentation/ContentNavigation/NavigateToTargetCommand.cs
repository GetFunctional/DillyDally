using System;
using DevExpress.Mvvm;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    public sealed class NavigateToTargetCommand : DelegateCommand<Guid>
    {
        #region Constructors

        public NavigateToTargetCommand(Action<Guid> executeMethod) : base(executeMethod)
        {
        }

        public NavigateToTargetCommand(Action<Guid> executeMethod, bool useCommandManager) : base(executeMethod,
            useCommandManager)
        {
        }

        public NavigateToTargetCommand(Action<Guid> executeMethod, Func<Guid, bool> canExecuteMethod,
            bool? useCommandManager = null) : base(executeMethod, canExecuteMethod, useCommandManager)
        {
        }

        #endregion
    }
}