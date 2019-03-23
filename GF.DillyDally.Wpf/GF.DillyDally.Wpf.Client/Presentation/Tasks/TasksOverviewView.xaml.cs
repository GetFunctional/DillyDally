using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    /// <summary>
    ///     Interaktionslogik für TasksOverviewView.xaml
    /// </summary>
    public partial class TasksOverviewView : IViewFor<TasksOverviewViewModel>
    {
        #region Constructors

        #region - Konstruktoren -

        public TasksOverviewView()
        {
            this.InitializeComponent();
        }

        #endregion

        #endregion

        #region Properties, Indexers

        #region - Properties oeffentlich -

        public TasksOverviewViewModel ViewModel
        {
            get { return (TasksOverviewViewModel) this.DataContext; }
        }

        #endregion

        #endregion
    }
}