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
        private string _definitionOfDone;
        private string _description;
        private DateTime? _dueDate;
        private IDisposable _imageCollectionChangedObservable;
        private ImageContainerViewModel _imagesContainerViewModel;
        private string _taskName;
        private byte[] _taskPreviewImageBytes;

        public DateTime? DueDate
        {
            get { return this._dueDate; }
            set { this.SetAndRaiseIfChanged(ref this._dueDate, value); }
        }

        public string TaskName
        {
            get { return this._taskName; }
            set { this.SetAndRaiseIfChanged(ref this._taskName, value); }
        }

        public string DefinitionOfDone
        {
            get { return this._definitionOfDone; }
            set { this.SetAndRaiseIfChanged(ref this._definitionOfDone, value); }
        }

        public string Description
        {
            get { return this._description; }
            set { this.SetAndRaiseIfChanged(ref this._description, value); }
        }

        public ActivityContainerViewModel ActivitiesViewModel
        {
            get { return this._activitiesViewModel; }
            internal set
            {
                var itemBefore = this._activitiesViewModel;
                if (this.SetAndRaiseIfChanged(ref this._activitiesViewModel, value))
                {
                    var newTab =
                        this.ReplaceActivityContainerTabItem(this.RightBottomTabContainerElements, value, itemBefore);

                    if (newTab != null)
                    {
                        this._activityCollectionChangedObservable?.Dispose();
                        this._activityCollectionChangedObservable =
                            value.WhenActivityCollectionChanged.Subscribe(c => newTab.RefreshBadgeText());
                    }
                }
            }
        }

        public byte[] TaskPreviewImageBytes
        {
            get { return this._taskPreviewImageBytes; }
            set { this.SetAndRaiseIfChanged(ref this._taskPreviewImageBytes, value); }
        }

        public ImageContainerViewModel ImagesContainerViewModel
        {
            get { return this._imagesContainerViewModel; }
            internal set
            {
                var itemBefore = this._imagesContainerViewModel;
                if (this.SetAndRaiseIfChanged(ref this._imagesContainerViewModel, value))
                {
                    var newTab =
                        this.ReplaceImageContainerTabItem(this.LeftBottomTabContainerElements, value, itemBefore);
                    if (newTab != null)
                    {
                        this._imageCollectionChangedObservable?.Dispose();
                        this._imageCollectionChangedObservable =
                            value.WhenImageCollectionChanged.Subscribe(c => newTab.RefreshBadgeText());
                    }
                }
            }
        }

        public ObservableCollection<ITabItem> RightBottomTabContainerElements { get; } =
            new ObservableCollection<ITabItem>();

        public ObservableCollection<ITabItem> RightTopTabContainerElements { get; } =
            new ObservableCollection<ITabItem>();

        public ObservableCollection<ITabItem> LeftBottomTabContainerElements { get; } =
            new ObservableCollection<ITabItem>();

        private ITabItem ReplaceTabItem(ObservableCollection<ITabItem> tabItemContainer,
            INotifyPropertyChanged newValue,
            INotifyPropertyChanged valueBefore, string title, Func<string> refreshBadgeFunction)
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

        private ITabItem ReplaceImageContainerTabItem(ObservableCollection<ITabItem> tabItemContainer,
            ImageContainerViewModel newValue,
            ImageContainerViewModel valueBefore)
        {
            var refreshBadgeTextFunction = new Func<string>(() => newValue.Images.Count.ToString());
            return this.ReplaceTabItem(tabItemContainer, newValue, valueBefore, "Images", refreshBadgeTextFunction);
        }

        private ITabItem ReplaceActivityContainerTabItem(ObservableCollection<ITabItem> tabItemContainer,
            ActivityContainerViewModel newValue,
            ActivityContainerViewModel valueBefore)
        {
            var refreshBadgeTextFunction = new Func<string>(() => newValue.Activities.Count.ToString());
            return this.ReplaceTabItem(tabItemContainer, newValue, valueBefore, "Activities", refreshBadgeTextFunction);
        }

        public void Dispose()
        {
            this._activitiesViewModel?.Dispose();
            this._activityCollectionChangedObservable?.Dispose();
            this._imageCollectionChangedObservable?.Dispose();
            this._imagesContainerViewModel?.Dispose();
        }
    }
}