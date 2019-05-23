using System.Collections.ObjectModel;
using GF.DillyDally.Mvvmc;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Activities
{
    public class ActivityContainerViewModel : ViewModelBase
    {
        private ObservableCollection<ActivityItemViewModel> _activities;

        public ActivityContainerViewModel()
        {
            this.Activities = new ObservableCollection<ActivityItemViewModel>();
        }


        public ObservableCollection<ActivityItemViewModel> Activities
        {
            get
            {
                return this._activities;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._activities, value);
            }
        }
    }
}