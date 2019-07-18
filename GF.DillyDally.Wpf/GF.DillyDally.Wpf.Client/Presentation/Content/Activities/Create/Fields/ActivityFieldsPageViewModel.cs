using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create.Fields
{
    internal class ActivityFieldsPageViewModel : DisplayPageViewModelBase
    {
        private readonly ObservableCollection<ActivityFieldItemViewModel> _activityFields;

        public ActivityFieldsPageViewModel(ICommand addNewFieldCommand)
        {
            this.Validator.AddValidationRule(new ActivityFieldNamesValidationRule());
            this.AddNewFieldCommand = addNewFieldCommand;
            this._activityFields = new ObservableCollection<ActivityFieldItemViewModel>();
        }

        public ICommand AddNewFieldCommand { get; set; }

        public override string Title
        {
            get { return "Activity Fields"; }
        }

        protected override bool BeforeValidate()
        {
            return this.ActivityFields.All(f => f.Validate())  && base.BeforeValidate();
        }

        public IReadOnlyList<ActivityFieldItemViewModel> ActivityFields
        {
            get { return this._activityFields; }
        }

        internal void AddNewActivityField(ActivityFieldItemViewModel activityFieldViewModel)
        {
            this._activityFields.Add(activityFieldViewModel);
        }

        public void RemoveActivityField(ActivityFieldItemViewModel activityFieldViewModel)
        {
            this._activityFields.Remove(activityFieldViewModel);
        }
    }
}