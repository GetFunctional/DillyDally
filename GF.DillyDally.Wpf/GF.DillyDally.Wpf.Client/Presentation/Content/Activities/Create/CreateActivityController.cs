using System;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc.Contracts;
using GF.DillyDally.ReadModel.Projection.Activities.Repository;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create.Fields;
using GF.DillyDally.WriteModel.Domain.Activities;
using ActivityFieldType = GF.DillyDally.WriteModel.Domain.Activities.ActivityFieldType;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create
{
    internal class CreateActivityController : DialogControllerBase<CreateActivityViewModel>
    {
        private readonly ActivityFieldViewModelFactory _activityFieldViewModelFactory;


        public CreateActivityController(CreateActivityViewModel viewModel, IControllerServices controllerServices)
            : base(viewModel, controllerServices)
        {
            viewModel.CreateActivityCommand =
                controllerServices.CommandFactory.CreateFromTask(async () => await this.CompleteProcess());
            viewModel.CancelProcessCommand = controllerServices.CommandFactory.CreateFromAction(this.CancelProcess);

            var addNewFieldCommand = controllerServices.CommandFactory.CreateFromAction(this.CreateNewActivityField);
            var removeFieldCommand =
                controllerServices.CommandFactory.CreateFromAction<ActivityFieldItemViewModel>(this.RemoveActivityField,
                    this.CanRemoveActivityField);
            this._activityFieldViewModelFactory =
                new ActivityFieldViewModelFactory(addNewFieldCommand, removeFieldCommand);

            var activityInfos = new ActivityInfosPageViewModel();
            var activityFields = new ActivityFieldsPageViewModel(addNewFieldCommand);
            viewModel.AddPage(activityInfos);
            viewModel.AddPage(activityFields);
        }

        public IDialogResult CreateActivityDialogResult { get; } = new DialogCommandResult();
        public IDialogResult CancelDialogResult { get; } = new DialogCommandResult();

        private bool CanRemoveActivityField(ActivityFieldItemViewModel cf)
        {
            return cf != null;
        }

        private void RemoveActivityField(ActivityFieldItemViewModel activityFieldViewModel)
        {
            var page = this.ViewModel.GetPage<ActivityFieldsPageViewModel>();
            page.RemoveActivityField(activityFieldViewModel);
        }

        private void CreateNewActivityField()
        {
            var page = this.ViewModel.GetPage<ActivityFieldsPageViewModel>();
            page.AddNewActivityField(this._activityFieldViewModelFactory.CreateTextFieldItem());
        }

        private void CancelProcess()
        {
            this.ConfirmDialogWith(this.CancelDialogResult);
        }

        private async Task CompleteProcess()
        {
            this.ViewModel.IsBusy = true;

            if (this.IsInputValid(this.ViewModel))
            {
                var step1 = this.ViewModel.GetPage<ActivityInfosPageViewModel>();
                var activityName = step1.ActivityName;
                var activityType = step1.SelectedActivityTypeViewModel?.ActivityType;
                var previewImageForActivity = step1.PreviewImageBytes;

                var step2 = this.ViewModel.GetPage<ActivityFieldsPageViewModel>();
                var activities = step2.ActivityFields;

                var activityService = this.ControllerServices.GetDomainService<ActivityService>();

                switch (activityType)
                {
                    case ActivityType.Percentage:
                        var activity =
                            await activityService.CreatePercentageActivityAsync(activityName,
                                previewImageForActivity);

                        foreach (var activityFieldViewModel in activities)
                        {
                            await activityService.AttachActivityFieldsAsync(activity.ActivityId, (ActivityFieldType)activityFieldViewModel.FieldType.ActivityFieldType,
                                activityFieldViewModel.FieldName, activityFieldViewModel.UnitOfMeasure);
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
            var step1 = viewModel.GetPage<ActivityInfosPageViewModel>();
            var resultStep1 = step1.Validate();

            var step2 = viewModel.GetPage<ActivityFieldsPageViewModel>();
            var resultStep2 = step2.Validate();

            return resultStep1 && resultStep2;
        }
    }
}