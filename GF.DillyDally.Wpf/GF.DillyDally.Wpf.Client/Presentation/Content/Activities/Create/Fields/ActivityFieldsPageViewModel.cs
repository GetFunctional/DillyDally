using System.Collections.Generic;
using System.Collections.ObjectModel;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create.Fields
{
    internal class ActivityFieldsPageViewModel : DisplayPageViewModelBase
    {
        private readonly AddNewFieldViewModel _addNewFieldViewModel;
        private readonly ObservableCollection<IActivityFieldViewModel> _activityFields;

        public ActivityFieldsPageViewModel(AddNewFieldViewModel addNewFieldViewModel)
        {
            this._addNewFieldViewModel = addNewFieldViewModel;
            this._activityFields = new ObservableCollection<IActivityFieldViewModel> {addNewFieldViewModel};
        }

        public override string Title
        {
            get { return "Activity Fields"; }
        }

        public IReadOnlyList<IActivityFieldViewModel> ActivityFields
        {
            get { return this._activityFields; }
        }

        internal void AddNewActivityField(IActivityFieldViewModel activityFieldViewModel)
        {
            this._activityFields.Add(activityFieldViewModel);
            this._activityFields.Move(this._activityFields.IndexOf(this._addNewFieldViewModel),
                this.ActivityFields.Count - 1);
        }

        public void RemoveActivityField(IActivityFieldViewModel activityFieldViewModel)
        {
            this._activityFields.Remove(activityFieldViewModel);
        }
    }
}