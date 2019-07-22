using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Subjects;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details
{
    public class TaskActivitiesViewModel : ViewModelBase
    {
        private readonly Subject<IReadOnlyList<TaskActivityItemViewModel>> _activityItemsChangedSubject =
            new Subject<IReadOnlyList<TaskActivityItemViewModel>>();

        private IReadOnlyList<TaskActivityItemViewModel> _activities;

        public TaskActivitiesViewModel(List<TaskActivityItemViewModel> taskActivityItems)
        {
            this.AssignActivities(taskActivityItems);
        }

        internal IObservable<IReadOnlyList<TaskActivityItemViewModel>> WhenActivityCollectionChanged
        {
            get
            {
                return this._activityItemsChangedSubject;
            }
        }

        public IReadOnlyList<TaskActivityItemViewModel> Activities
        {
            get
            {
                return this._activities;
            }
            private set
            {
                if (this.SetAndRaiseIfChanged(ref this._activities, value))
                {
                    this._activityItemsChangedSubject.OnNext(value);
                }
            }
        }

        public void AssignActivities(IEnumerable<TaskActivityItemViewModel> activityItemViewModels)
        {
            this.Activities = new ObservableCollection<TaskActivityItemViewModel>(activityItemViewModels);
        }
    }
}