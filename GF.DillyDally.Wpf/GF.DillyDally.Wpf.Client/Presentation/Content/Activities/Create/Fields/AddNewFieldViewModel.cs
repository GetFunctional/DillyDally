using System.Windows.Input;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create.Fields
{
    public class AddNewFieldViewModel : IActivityFieldViewModel
    {
        public AddNewFieldViewModel(ICommand addNewFieldCommand)
        {
            this.AddNewFieldCommand = addNewFieldCommand;
        }

        public ICommand AddNewFieldCommand { get; }
    }
}