﻿using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public class TasksOverviewViewModel : ViewModelBase
    {
        private TasksListViewModel _openTasksListViewModel;
        private TasksListViewModel _recentlyCompletedTasksListViewModel;
        private TasksListViewModel _repeatableTasksListViewModel;

        public TasksListViewModel OpenTasksListViewModel
        {
            get { return this._openTasksListViewModel; }
            set { this.SetField(ref this._openTasksListViewModel, value); }
        }

        public TasksListViewModel RepeatableTasksListViewModel
        {
            get { return this._repeatableTasksListViewModel; }
            set { this.SetField(ref this._repeatableTasksListViewModel, value); }
        }

        public TasksListViewModel RecentlyCompletedTasksListViewModel
        {
            get { return this._recentlyCompletedTasksListViewModel; }
            set { this.SetField(ref this._recentlyCompletedTasksListViewModel, value); }
        }
    }
}