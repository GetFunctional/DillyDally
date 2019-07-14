using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create.Fields
{
    internal class ActivityFieldsPageViewModel : DisplayPageViewModelBase
    {
        private readonly ObservableCollection<IActivityFieldViewModel> _activityFields;

        public ActivityFieldsPageViewModel(ICommand addNewFieldCommand)
        {
            this.AddNewFieldCommand = addNewFieldCommand;
            this._activityFields = new ObservableCollection<IActivityFieldViewModel>();
        }

        public ICommand AddNewFieldCommand { get; set; }

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
        }

        public void RemoveActivityField(IActivityFieldViewModel activityFieldViewModel)
        {
            this._activityFields.Remove(activityFieldViewModel);
        }
    }
}