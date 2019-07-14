using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create.Fields
{
    public class ActivityFieldTypeViewModel : ViewModelBase
    {
        public ActivityFieldTypeViewModel(ActivityFieldType activityFieldType, string typeName)
        {
            this.ActivityFieldType = activityFieldType;
            this.TypeName = typeName;
        }

        public ActivityFieldType ActivityFieldType { get; }

        public string TypeName { get; }
    }
}