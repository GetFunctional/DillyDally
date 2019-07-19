using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Container;
using GF.DillyDally.Wpf.Client.Presentation.Content.Images.Container;
using GF.DillyDally.Wpf.Theme.Controls.Layout;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details
{
    public sealed class TaskDetailsViewModel : ViewModelBase, IDisposable
    {
        private ActivityContainerViewModel _activitiesViewModel;
        private IDisposable _activityCollectionChangedObservable;
        private IDisposable _imageCollectionChangedObservable;
        private ImageContainerViewModel _imagesContainerViewModel;
        private TaskSummaryViewModel _taskSummaryViewModel;


        public ActivityContainerViewModel ActivitiesViewModel
        {
            get
            {
                return this._activitiesViewModel;
            }
            private set
            {
                this.SetAndRaiseIfChanged(ref this._activitiesViewModel, value);
            }
        }


        public TaskSummaryViewModel TaskSummaryViewModel
        {
            get
            {
                return this._taskSummaryViewModel;
            }
            private set
            {
                this.SetAndRaiseIfChanged(ref this._taskSummaryViewModel, value);
            }
        }

        public ImageContainerViewModel ImagesContainerViewModel
        {
            get
            {
                return this._imagesContainerViewModel;
            }
            private set
            {
                this.SetAndRaiseIfChanged(ref this._imagesContainerViewModel, value);
            }
        }

        public ObservableCollection<ITabItem> RightBottomTabContainerElements { get; } =
            new ObservableCollection<ITabItem>();

        public ObservableCollection<ITabItem> RightTopTabContainerElements { get; } =
            new ObservableCollection<ITabItem>();

        public ObservableCollection<ITabItem> LeftBottomTabContainerElements { get; } =
            new ObservableCollection<ITabItem>();

        public ObservableCollection<ITabItem> LeftTopTabContainerElements { get; } =
            new ObservableCollection<ITabItem>();

        #region IDisposable Members

        public void Dispose()
        {
            this._activitiesViewModel?.Dispose();
            this._activityCollectionChangedObservable?.Dispose();
            this._imageCollectionChangedObservable?.Dispose();
            this._imagesContainerViewModel?.Dispose();
        }

        #endregion

        private ITabItem ReplaceTabItem(ObservableCollection<ITabItem> tabItemContainer,
            INotifyPropertyChanged newValue,
            INotifyPropertyChanged valueBefore, string title, Func<string> refreshBadgeFunction = null)
        {
            if (valueBefore != null)
            {
                tabItemContainer.Remove(tabItemContainer.Single(x => Equals(x.Content, valueBefore)));
            }

            if (newValue != null)
            {
                var newTabItem = new TabItem(newValue, title, refreshBadgeFunction);
                tabItemContainer.Add(newTabItem);
                newTabItem.RefreshBadgeText();
                return newTabItem;
            }

            return null;
        }

        internal void ReplaceTaskSummaryContainerTabItem(TaskSummaryViewModel newValue)
        {
            var itemBefore = this._taskSummaryViewModel;
            if (itemBefore != newValue)
            {
                this.ReplaceTabItem(this.LeftTopTabContainerElements, newValue, itemBefore, "Summary");
                this.TaskSummaryViewModel = newValue;
            }
        }

        internal void ReplaceImageContainerTabItem(ImageContainerViewModel newValue)
        {
            var itemBefore = this._imagesContainerViewModel;
            if (itemBefore != newValue)
            {
                var refreshBadgeTextFunction = new Func<string>(() => newValue.Images.Count.ToString());
                var newTab = this.ReplaceTabItem(this.LeftBottomTabContainerElements, newValue, itemBefore, "Images", refreshBadgeTextFunction);

                if (newTab != null)
                {
                    this._imageCollectionChangedObservable?.Dispose();
                    this._imageCollectionChangedObservable =
                        newValue.WhenImageCollectionChanged.Subscribe(c => newTab.RefreshBadgeText());
                }

                this.ImagesContainerViewModel = newValue;
            }
        }

        internal void ReplaceActivityContainerTabItem(ActivityContainerViewModel newValue)
        {
            var itemBefore = this._activitiesViewModel;
            if (itemBefore != newValue)
            {
                var newTab = this.ReplaceTabItem(this.RightBottomTabContainerElements, newValue, itemBefore, "Activities");

                if (newTab != null)
                {
                    this._activityCollectionChangedObservable?.Dispose();
                    this._activityCollectionChangedObservable =
                        newValue.WhenActivityCollectionChanged.Subscribe(c => newTab.RefreshBadgeText());
                }

                this.ActivitiesViewModel = newValue;
            }
        }
    }
}