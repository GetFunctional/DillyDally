﻿using System.Collections.ObjectModel;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public class TasksListViewModel : ViewModelBase
    {
        #region - Felder privat -

        private ObservableCollection<TaskViewModel> _tasks;

        #endregion

        #region - Properties oeffentlich -

        public ObservableCollection<TaskViewModel> Tasks
        {
            get
            {
                return this._tasks;
            }
            set
            {
                this.SetField(ref this._tasks, value);
            }
        }

        #endregion
    }
}