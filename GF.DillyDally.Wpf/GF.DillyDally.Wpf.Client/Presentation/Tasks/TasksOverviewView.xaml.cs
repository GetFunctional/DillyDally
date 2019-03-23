using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    /// <summary>
    ///     Interaktionslogik für TasksOverviewView.xaml
    /// </summary>
    public partial class TasksOverviewView : IViewFor<TasksOverviewViewModel>
    {
        public TasksOverviewView()
        {
            this.InitializeComponent();
        }

        #region IViewFor<TasksOverviewViewModel> Members

        public TasksOverviewViewModel ViewModel
        {
            get { return (TasksOverviewViewModel) this.DataContext; }
        }

        #endregion
    }
}