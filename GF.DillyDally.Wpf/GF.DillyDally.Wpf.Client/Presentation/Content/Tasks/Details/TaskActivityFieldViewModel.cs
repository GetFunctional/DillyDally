using System;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details
{
    public class TaskActivityFieldViewModel : ViewModelBase
    {
        public Guid ActivityFieldId { get; }
        public string FieldName { get; }
        public string UnitOfMeasure { get; }

        protected TaskActivityFieldViewModel(Guid activityFieldId, string fieldName, string unitOfMeasure)
        {
            this.ActivityFieldId = activityFieldId;
            this.FieldName = fieldName;
            this.UnitOfMeasure = unitOfMeasure;
        }
    }
}