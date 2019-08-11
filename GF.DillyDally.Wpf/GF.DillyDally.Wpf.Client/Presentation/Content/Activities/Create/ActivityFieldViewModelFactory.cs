using System.Windows.Input;
using GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create.Fields;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create
{
    internal sealed class ActivityFieldViewModelFactory
    {
        private readonly ICommand _addNewFieldCommand;
        private readonly ICommand _removeFieldCommand;

        public ActivityFieldViewModelFactory(ICommand addNewFieldCommand, ICommand removeFieldCommand)
        {
            this._addNewFieldCommand = addNewFieldCommand;
            this._removeFieldCommand = removeFieldCommand;
        }
        
        //internal ActivityFieldItemViewModel CreateTextFieldItem()
        //{
        //    return new ActivityFieldItemViewModel(ActivityFieldType.Text, this._removeFieldCommand);
        //}
    }
}