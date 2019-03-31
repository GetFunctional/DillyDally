using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    /// <summary>
    ///     Interaktionslogik für CreateTaskView.xaml
    /// </summary>
    public partial class CreateTaskView : IViewFor<CreateTaskViewModel>
    {
        public CreateTaskView()
        {
            this.InitializeComponent();
        }

        #region IViewFor<CreateTaskViewModel> Members

        public CreateTaskViewModel ViewModel
        {
            get { return (CreateTaskViewModel) this.DataContext; }
        }

        #endregion
    }
}