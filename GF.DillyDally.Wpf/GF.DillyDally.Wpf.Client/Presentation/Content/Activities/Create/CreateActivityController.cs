using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.ReadModel.Projection.Activities.Repository;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.WriteModel.Domain.Activities.Commands;
using MediatR;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create
{
    public class CreateActivityController : DialogControllerBase<CreateActivityViewModel>
    {
        private readonly IMediator _mediator;

        public CreateActivityController(CreateActivityViewModel viewModel, IMediator mediator) :
            base(viewModel)
        {
            this._mediator = mediator;

            viewModel.CreateActivityCommand =
                ReactiveCommand.CreateFromTask(async () => await this.CompleteProcess());
            viewModel.CancelProcessCommand =
                ReactiveCommand.Create(this.CancelProcess);

            viewModel.AvailableActivityTypes = new ObservableCollection<ActivityTypeViewModel>
                                               {
                                                   new ActivityTypeViewModel(ActivityType.Percentage),
                                                   new ActivityTypeViewModel(ActivityType.Leveling)
                                               };
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
                var activityName = this.ViewModel.ActivityName;
                var activityType = this.ViewModel.SelectedActivityTypeViewModel.ActivityType;
                var previewImageForActivity = this.ViewModel.PreviewImageBytes;

                var commandDispatcher = this._mediator;

                switch (activityType)
                {
                    case ActivityType.Percentage:
                        if (previewImageForActivity != null)
                        {
                            var activity = await commandDispatcher.Send(new CreatePercentageActivityCommand(activityName, previewImageForActivity));
                        }
                        else
                        {
                            var activity = await commandDispatcher.Send(new CreatePercentageActivityCommand(activityName));
                        }

                        this.ConfirmDialogWith(this.CreateActivityDialogResult);
                        break;
                    case ActivityType.Leveling:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            this.ViewModel.IsBusy = false;
        }

        private bool IsInputValid(CreateActivityViewModel viewModel)
        {
            return viewModel.ActivityName != string.Empty && viewModel.SelectedActivityTypeViewModel != null;
        }
    }
}