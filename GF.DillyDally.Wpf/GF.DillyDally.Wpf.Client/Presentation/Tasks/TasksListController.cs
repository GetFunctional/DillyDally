using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using Task = GF.DillyDally.Data.Tasks.Task;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public class TasksListController : ControllerBase<TasksListViewModel>
    {
        #region Fields, Constants

        #region - Felder privat -

        private readonly TaskViewModelFactory _taskViewModelFactory = new TaskViewModelFactory();

        #endregion

        #endregion

        #region Constructors

        #region - Konstruktoren -

        public TasksListController(TasksListViewModel viewModel) : base(viewModel)
        {
        }

        #endregion

        #endregion

        #region Properties, Indexers

        #region - Properties oeffentlich -

        public Func<System.Threading.Tasks.Task<IList<Task>>> DataSource { get; set; }

        #endregion

        #endregion

        #region - Methoden oeffentlich -

        public async Task<IList<TaskViewModel>> LoadDataAsync()
        {
            var data = await this.DataSource();
            this.ViewModel.Tasks =
                new ObservableCollection<TaskViewModel>(data.Select(t => this._taskViewModelFactory.CreateFromTask(t)));
            return this.ViewModel.Tasks;
        }

        #endregion
    }
}