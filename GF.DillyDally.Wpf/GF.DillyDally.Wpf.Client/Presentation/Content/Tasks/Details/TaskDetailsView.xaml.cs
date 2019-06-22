using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details
{
    /// <summary>
    ///     Interaction logic for TaskDetailsView.xaml
    /// </summary>
    public partial class TaskDetailsView : IViewFor<TaskDetailsViewModel>
    {
        public TaskDetailsView()
        {
            this.InitializeComponent();
        }

        #region IViewFor<TaskDetailsViewModel> Members

        public TaskDetailsViewModel ViewModel
        {
            get { return (TaskDetailsViewModel) this.DataContext; }
        }

        #endregion
    }
}