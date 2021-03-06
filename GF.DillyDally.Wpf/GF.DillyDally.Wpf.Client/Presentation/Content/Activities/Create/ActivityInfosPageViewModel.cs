using System.Collections.ObjectModel;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create
{
    public class ActivityInfosPageViewModel : DisplayPageViewModelBase
    {
        private string _activityName;
        private ObservableCollection<ActivityTypeViewModel> _availableActivityTypes;
        private byte[] _previewImageBytes;
        private ActivityTypeViewModel _selectedActivityTypeViewModel;

        public ActivityInfosPageViewModel()
        {
            //this.Validator.AddValidationRule(new CreateActivityValidationRule());
            //this.AvailableActivityTypes = new ObservableCollection<ActivityTypeViewModel>
            //{
            //    new ActivityTypeViewModel(ActivityType.Percentage),
            //    new ActivityTypeViewModel(ActivityType.Leveling)
            //};
        }

        public ObservableCollection<ActivityTypeViewModel> AvailableActivityTypes
        {
            get { return this._availableActivityTypes; }
            set { this.SetAndRaiseIfChanged(ref this._availableActivityTypes, value); }
        }

        public string ActivityName
        {
            get { return this._activityName; }
            set { this.SetAndRaiseIfChanged(ref this._activityName, value); }
        }

        public byte[] PreviewImageBytes
        {
            get { return this._previewImageBytes; }
            set { this.SetAndRaiseIfChanged(ref this._previewImageBytes, value); }
        }

        public ActivityTypeViewModel SelectedActivityTypeViewModel
        {
            get { return this._selectedActivityTypeViewModel; }
            set { this.SetAndRaiseIfChanged(ref this._selectedActivityTypeViewModel, value); }
        }

        public override string Title
        {
            get { return "Activity infos"; }
        }
    }
}