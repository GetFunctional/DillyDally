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
    public sealed class TaskDetailsViewModel : ViewModelBase
    {
        private ActivityContainerViewModel _activitiesViewModel;
        private string _definitionOfDone;
        private string _description;
        private DateTime? _dueDate;
        private ImageContainerViewModel _imagesContainerViewModel;
        private string _taskName;
        private byte[] _taskPreviewImageBytes;

        public DateTime? DueDate
        {
            get
            {
                return this._dueDate;
            }
            set
            {
                this.SetAndRaiseIfChanged(ref this._dueDate, value);
            }
        }

        public string TaskName
        {
            get
            {
                return this._taskName;
            }
            set
            {
                this.SetAndRaiseIfChanged(ref this._taskName, value);
            }
        }

        public string DefinitionOfDone
        {
            get
            {
                return this._definitionOfDone;
            }
            set
            {
                this.SetAndRaiseIfChanged(ref this._definitionOfDone, value);
            }
        }

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this.SetAndRaiseIfChanged(ref this._description, value);
            }
        }

        public ActivityContainerViewModel ActivitiesViewModel
        {
            get
            {
                return this._activitiesViewModel;
            }
            internal set
            {
                var itemBefore = this._activitiesViewModel;
                if (this.SetAndRaiseIfChanged(ref this._activitiesViewModel, value))
                {
                    this.ReplaceTabItem(this.RightBottomTabContainerElements, value, itemBefore, "Activities");
                }
            }
        }

        public byte[] TaskPreviewImageBytes
        {
            get
            {
                return this._taskPreviewImageBytes;
            }
            set
            {
                this.SetAndRaiseIfChanged(ref this._taskPreviewImageBytes, value);
            }
        }

        public ImageContainerViewModel ImagesContainerViewModel
        {
            get
            {
                return this._imagesContainerViewModel;
            }
            internal set
            {
                var itemBefore = this._imagesContainerViewModel;
                if (this.SetAndRaiseIfChanged(ref this._imagesContainerViewModel, value))
                {
                    this.ReplaceTabItem(this.LeftBottomTabContainerElements, value, itemBefore, "Images");
                }
            }
        }

        public ObservableCollection<ITabItem> RightBottomTabContainerElements { get; } = new ObservableCollection<ITabItem>();

        public ObservableCollection<ITabItem> RightTopTabContainerElements { get; } = new ObservableCollection<ITabItem>();

        public ObservableCollection<ITabItem> LeftBottomTabContainerElements { get; } = new ObservableCollection<ITabItem>();

        private void ReplaceTabItem(ObservableCollection<ITabItem> tabItemContainer, INotifyPropertyChanged newValue,
            INotifyPropertyChanged valueBefore, string title)
        {
            if (valueBefore != null)
            {
                tabItemContainer.Remove(tabItemContainer.Single(x => Equals(x.Content, valueBefore)));
            }

            if (newValue != null)
            {
                tabItemContainer.Add(new TabItem(newValue, title));
            }
        }
    }
}