using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.RewardSystem
{
    /// <summary>
    ///     Interaktionslogik für TasksListView.xaml
    /// </summary>
    public partial class TasksListView : IViewFor<TasksListViewModel>
    {
        #region - Konstruktoren -

        public TasksListView()
        {
            this.InitializeComponent();
        }

        #endregion

        #region - Properties oeffentlich -

        public TasksListViewModel ViewModel
        {
            get { return (TasksListViewModel) this.DataContext; }
        }

        #endregion
    }
}