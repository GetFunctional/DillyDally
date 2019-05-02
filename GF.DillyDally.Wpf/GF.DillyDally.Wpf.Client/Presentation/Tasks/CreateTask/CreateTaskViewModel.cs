using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Common.Rewards;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks.CreateTask
{
    public sealed class CreateTaskViewModel : ViewModelBase
    {
        public CreateTaskViewModel()
        {
            this.Rewards = new ObservableCollection<RewardViewModel>();
        }

        public TaskKey TaskKey { get; set; }

        public string TaskName { get; set; }

        public string Description { get; set; }

        public TaskType TaskType { get; set; }

        public DateTime? DueDate { get; set; }

        public ObservableCollection<RewardViewModel> Rewards { get; set; }

        public IList<SelectRewardViewModel> AvailableRewards
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}