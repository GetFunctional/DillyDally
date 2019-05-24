using System;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.ContentNavigation;
using GF.DillyDally.Wpf.Client.Presentation.HeaderMenu;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation
{
    public sealed class ShellViewModel : ViewModelBase
    {
        private ContentBrowserViewModel _contentBrowserViewModel;
        private HeaderMenuViewModel _headerMenuViewModel;
        private OverlayViewModel _overlayViewModel;
        private IReactiveCommand _openTaskboardCommand;
        private IReactiveCommand _createNewActivityCommand;
        private IReactiveCommand _createNewTaskCommand;

        public ContentBrowserViewModel ContentBrowserViewModel
        {
            get
            {
                return this._contentBrowserViewModel;
            }
            set
            {
                if (this.RaiseAndSetIfChanged(ref this._contentBrowserViewModel, value) == value)
                {
                    this.RaisePropertyChanged(nameof(ContentNavigation.ContentNavigatorViewModel));
                }
            }
        }

        public ContentNavigatorViewModel ContentNavigatorViewModel
        {
            get
            {
                return this._contentBrowserViewModel.CurrentActiveNavigator;
            }
        }

        public HeaderMenuViewModel HeaderMenuViewModel
        {
            get
            {
                return this._headerMenuViewModel;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._headerMenuViewModel, value);
            }
        }

        public OverlayViewModel OverlayViewModel
        {
            get
            {
                return this._overlayViewModel;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._overlayViewModel, value);
            }
        }

        public Guid TaskBoardNavigationTarget
        {
            get
            {
                return Content.Tasks.TaskBoard.TaskBoardNavigationTarget.TargetId;
            }
        }

        public IReactiveCommand OpenTaskboardCommand
        {
            get
            {
                return this._openTaskboardCommand;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._openTaskboardCommand, value);
            }
        }

        public IReactiveCommand CreateNewActivityCommand
        {
            get
            {
                return this._createNewActivityCommand;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._createNewActivityCommand, value);
            }
        }

        public IReactiveCommand CreateNewTaskCommand
        {
            get
            {
                return this._createNewTaskCommand;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._createNewTaskCommand, value);
            }
        }
    }
}