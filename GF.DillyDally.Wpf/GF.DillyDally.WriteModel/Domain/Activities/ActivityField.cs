namespace GF.DillyDally.WriteModel.Domain.Activities
{
    internal class ActivityField
    {
        public ActivityFieldType FieldType { get; }
        public string Name { get; }
        public string UnitOfMeasure { get; }

        public ActivityField(ActivityFieldType fieldType, string name, string unitOfMeasure)
        {
            this.FieldType = fieldType;
            this.Name = name;
            this.UnitOfMeasure = unitOfMeasure;
        }
    }
}