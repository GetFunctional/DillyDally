using System.Collections.ObjectModel;
using System.ComponentModel;
using GF.DillyDally.Mvvmc;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create
{
    public class CreateActivityViewModel : ViewModelBase
    {
        private string _activityName;
        private ObservableCollection<ActivityTypeViewModel> _availableActivityTypes;
        private IReactiveCommand _cancelProcessCommand;
        private IReactiveCommand _createActivityCommand;
        private byte[] _previewImageBytes;
        private ActivityTypeViewModel _selectedActivityTypeViewModel;

        public IReactiveCommand CreateActivityCommand
        {
            get
            {
                return this._createActivityCommand;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._createActivityCommand, value);
            }
        }

        public IReactiveCommand CancelProcessCommand
        {
            get
            {
                return this._cancelProcessCommand;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._cancelProcessCommand, value);
            }
        }

        public string ActivityName
        {
            get
            {
                return this._activityName;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._activityName, value);
            }
        }

        public ActivityTypeViewModel SelectedActivityTypeViewModel
        {
            get
            {
                return this._selectedActivityTypeViewModel;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._selectedActivityTypeViewModel, value);
            }
        }

        public ObservableCollection<ActivityTypeViewModel> AvailableActivityTypes
        {
            get
            {
                return this._availableActivityTypes;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._availableActivityTypes, value);
            }
        }

        public byte[] PreviewImageBytes
        {
            get
            {
                return this._previewImageBytes;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._previewImageBytes, value);
            }
        }
    }
}