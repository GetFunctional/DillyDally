using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.TaskBoard
{
    /// <summary>
    ///     Interaktionslogik für ShowCaseView.xaml
    /// </summary>
    public partial class TaskBoardView : IViewFor<TaskBoardViewModel>
    {
        public TaskBoardView()
        {
            this.InitializeComponent();
        }

        #region IViewFor<TaskBoardViewModel> Members

        public TaskBoardViewModel ViewModel
        {
            get { return (TaskBoardViewModel) this.DataContext; }
        }

        #endregion
    }
}