using System.Collections.ObjectModel;
using GF.DillyDally.Mvvmc;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create
{
    public class CreateActvityStep1ViewModel : DisplayPageViewModelBase
    {
        private string _activityName;
        private ObservableCollection<ActivityTypeViewModel> _availableActivityTypes;
        private byte[] _previewImageBytes;
        private ActivityTypeViewModel _selectedActivityTypeViewModel;


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

        #region IDisplayPage Members

        public override string Title
        {
            get
            {
                return "Activity infos";
            }
        }

        #endregion
    }
}