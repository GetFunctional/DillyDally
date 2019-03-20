using GF.DillyDally.Wpf.Client.Core;

namespace GF.DillyDally.Wpf.Client.RewardSystem
{
    /// <summary>
    /// Interaktionslogik für TasksListView.xaml
    /// </summary>
    public partial class TasksListView : IViewFor<TasksListViewModel>
    {
        #region - Konstruktoren -

        public TasksListView()
        {
            InitializeComponent();
        }

        #endregion

        #region - Properties oeffentlich -

        public TasksListViewModel ViewModel
        {
            get
            {
                return (TasksListViewModel)this.DataContext;
            }
        }

        #endregion
    }
}