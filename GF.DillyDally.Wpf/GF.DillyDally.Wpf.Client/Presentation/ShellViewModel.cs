using System;
using System.Windows.Input;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.ContentNavigation;
using GF.DillyDally.Wpf.Client.Presentation.HeaderMenu;

namespace GF.DillyDally.Wpf.Client.Presentation
{
    public sealed class ShellViewModel : ViewModelBase
    {
        private ContentBrowserViewModel _contentBrowserViewModel;
        private ICommand _createNewActivityCommand;
        private ICommand _createNewTaskCommand;
        private HeaderMenuViewModel _headerMenuViewModel;
        private ICommand _navigateInNavigatorCommand;
        private OverlayViewModel _overlayViewModel;

        public ContentBrowserViewModel ContentBrowserViewModel
        {
            get { return this._contentBrowserViewModel; }
            set
            {
                if (this.SetAndRaiseIfChanged(ref this._contentBrowserViewModel, value))
                {
                    this.RaisePropertyChanged(nameof(ContentNavigation.ContentNavigatorViewModel));
                }
            }
        }

        public ContentNavigatorViewModel ContentNavigatorViewModel
        {
            get { return this._contentBrowserViewModel.CurrentActiveNavigator; }
        }

        public HeaderMenuViewModel HeaderMenuViewModel
        {
            get { return this._headerMenuViewModel; }
            set { this.SetAndRaiseIfChanged(ref this._headerMenuViewModel, value); }
        }

        public OverlayViewModel OverlayViewModel
        {
            get { return this._overlayViewModel; }
            set { this.SetAndRaiseIfChanged(ref this._overlayViewModel, value); }
        }

        public Guid TaskBoardNavigationTarget
        {
            get { return Content.Tasks.TaskBoard.TaskBoardNavigationTarget.TargetId; }
        }

        public ICommand NavigateInNavigatorCommand
        {
            get { return this._navigateInNavigatorCommand; }
            set { this.SetAndRaiseIfChanged(ref this._navigateInNavigatorCommand, value); }
        }

        public ICommand CreateNewActivityCommand
        {
            get { return this._createNewActivityCommand; }
            set { this.SetAndRaiseIfChanged(ref this._createNewActivityCommand, value); }
        }

        public ICommand CreateNewTaskCommand
        {
            get { return this._createNewTaskCommand; }
            set { this.SetAndRaiseIfChanged(ref this._createNewTaskCommand, value); }
        }

        public Guid SearchContentNavigationTarget
        {
            get { return ContentNavigation.SearchContentNavigationTarget.TargetId; }
        }
    }
}