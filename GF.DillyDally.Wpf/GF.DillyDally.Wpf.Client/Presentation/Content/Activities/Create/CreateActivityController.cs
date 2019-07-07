using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc.Contracts;
using GF.DillyDally.ReadModel.Projection.Activities.Repository;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;
using GF.DillyDally.WriteModel.Domain.Activities;


namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create
{
    internal class CreateActivityController : DialogControllerBase<CreateActivityViewModel>
    {
        public CreateActivityController(CreateActivityViewModel viewModel, IControllerServices controllerServices)
            : base(viewModel, controllerServices)
        {
            viewModel.CreateActivityCommand = controllerServices.CommandFactory.CreateFromTask(async () => await this.CompleteProcess());
            viewModel.CancelProcessCommand =controllerServices.CommandFactory.CreateFromAction(this.CancelProcess);

            var step1 = new CreateActvityStep1ViewModel();
            step1.AvailableActivityTypes = new ObservableCollection<ActivityTypeViewModel>
                                           {
                                               new ActivityTypeViewModel(ActivityType.Percentage),
                                               new ActivityTypeViewModel(ActivityType.Leveling)
                                           };

            viewModel.AddPage(step1);
        }

        public IDialogResult CreateActivityDialogResult { get; } = new DialogCommandResult();
        public IDialogResult CancelDialogResult { get; } = new DialogCommandResult();

        private void CancelProcess()
        {
            this.ConfirmDialogWith(this.CancelDialogResult);
        }

        private async Task CompleteProcess()
        {
            this.ViewModel.IsBusy = true;

            if (this.IsInputValid(this.ViewModel))
            {
                var step1 = this.ViewModel.GetPage<CreateActvityStep1ViewModel>();
                var activityName = step1.ActivityName;
                var activityType = step1.SelectedActivityTypeViewModel?.ActivityType;
                var previewImageForActivity = step1.PreviewImageBytes;
                var activityService = this.ControllerServices.GetDomainService<ActivityService>();

                switch (activityType)
                {
                    case ActivityType.Percentage:
                        if (previewImageForActivity != null)
                        {
                            var activity = await activityService.CreatePercentageActivityAsync(activityName, previewImageForActivity);
                        }
                        else
                        {
                            var activity = await activityService.CreatePercentageActivityAsync(activityName);
                        }

                        this.ConfirmDialogWith(this.CreateActivityDialogResult);
                        break;
                    case ActivityType.Leveling:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                foreach (var viewModelPage in this.ViewModel.Pages)
                {
                    viewModelPage.Validate();
                }
            }

            this.ViewModel.IsBusy = false;
        }

        private bool IsInputValid(CreateActivityViewModel viewModel)
        {
            var step1 = viewModel.GetPage<CreateActvityStep1ViewModel>();
            var activityName = step1.ActivityName;
            var activityType = step1.SelectedActivityTypeViewModel?.ActivityType;
            return activityName != string.Empty && activityType != null;
        }
    }
}