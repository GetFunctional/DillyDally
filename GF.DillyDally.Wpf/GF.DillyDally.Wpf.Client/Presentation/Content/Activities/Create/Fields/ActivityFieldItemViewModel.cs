using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create.Fields
{
    public sealed class ActivityFieldItemViewModel : ViewModelBase, IActivityFieldViewModel
    {
        private string _fieldName;
        private ActivityFieldTypeViewModel _fieldType;
        private string _unitOfMeasure;

        public ActivityFieldItemViewModel(ActivityFieldType fieldType, ICommand removeFieldCommand)
        {
            this.RemoveFieldCommand = removeFieldCommand;
            this.AvailableFieldTypes = this.CreateAvailableActivityTypes();
            this.FieldType = this.AvailableFieldTypes.Single(x => x.ActivityFieldType == fieldType);
        }

        public string FieldName
        {
            get { return this._fieldName; }
            set { this.SetAndRaiseIfChanged(ref this._fieldName, value); }
        }

        public ActivityFieldTypeViewModel FieldType
        {
            get { return this._fieldType; }
            set { this.SetAndRaiseIfChanged(ref this._fieldType, value); }
        }

        public string UnitOfMeasure
        {
            get { return this._unitOfMeasure; }
            set { this.SetAndRaiseIfChanged(ref this._unitOfMeasure, value); }
        }

        public IReadOnlyList<ActivityFieldTypeViewModel> AvailableFieldTypes { get; }

        public ICommand RemoveFieldCommand { get; }

        private IReadOnlyList<ActivityFieldTypeViewModel> CreateAvailableActivityTypes()
        {
            return new List<ActivityFieldTypeViewModel>
            {
                new ActivityFieldTypeViewModel(ActivityFieldType.Text, "Text"),
                new ActivityFieldTypeViewModel(ActivityFieldType.Date, "Date"),
                new ActivityFieldTypeViewModel(ActivityFieldType.DateTime, "Date & Time"),
                new ActivityFieldTypeViewModel(ActivityFieldType.Integer, "Integer value"),
                new ActivityFieldTypeViewModel(ActivityFieldType.Decimal, "Decimal value")
            };
        }
    }
}