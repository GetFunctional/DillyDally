using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.WriteModel.Domain.Activities.Commands;
using GF.DillyDally.WriteModel.Domain.Files;
using GF.DillyDally.WriteModel.Domain.Files.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Activities
{
    internal sealed class ActivityCommandHandler : CommandHandlerBase,
        IRequestHandler<CreatePercentageActivityCommand, CreatePercentageActivityResponse>,
        IRequestHandler<CreateActivityListCommand, CreateActivityListResponse>,
        IRequestHandler<CanCreateActivityCommand, CanCreateActivityResponse>
    {
        private readonly DatabaseFileHandler _databaseFileHandler;

        public ActivityCommandHandler(IAggregateRepository aggregateRepository, DatabaseFileHandler databaseFileHandler)
            : base(aggregateRepository)
        {
            this._databaseFileHandler = databaseFileHandler;
        }

        #region IRequestHandler<CanCreateActivityCommand,CanCreateActivityResponse> Members

        public Task<CanCreateActivityResponse> Handle(CanCreateActivityCommand request,
            CancellationToken cancellationToken)
        {
            var activityList =
                this.AggregateRepository.GetById<ActivityListAggregateRoot>(ActivityListAggregateRoot.ActivityListId);

            if (activityList.HasActivityWithName(request.Name))
            {
                return Task.FromResult(new CanCreateActivityResponse(false));
            }

            return Task.FromResult(new CanCreateActivityResponse(true));
        }

        #endregion

        #region IRequestHandler<CreateActivityListCommand,CreateActivityListResponse> Members

        public async Task<CreateActivityListResponse> Handle(CreateActivityListCommand request,
            CancellationToken cancellationToken)
        {
            var activity = ActivityListAggregateRoot.Create();
            await this.AggregateRepository.SaveAsync(activity);
            return new CreateActivityListResponse();
        }

        #endregion

        #region IRequestHandler<CreatePercentageActivityCommand,CreatePercentageActivityResponse> Members

        public async Task<CreatePercentageActivityResponse> Handle(CreatePercentageActivityCommand request,
            CancellationToken cancellationToken)
        {
            var activityId = this.GuidGenerator.GenerateGuid();
            var activity = ActivityAggregateRoot.Create(activityId, request.Name, ActivityType.Percentage);
            var activityList =
                this.AggregateRepository.GetById<ActivityListAggregateRoot>(ActivityListAggregateRoot.ActivityListId);
            activityList.AddActivity(activity.AggregateId, activity.Name);

            if (request.PreviewImageBytes != null)
            {
                using (var connection = this._databaseFileHandler.OpenConnection())
                {
                    var fileCreateCommand = new StoreFileCommand(request.PreviewImageBytes);
                    var response = await FileCommandHandler.GetOrCreateFileAsync(fileCreateCommand,
                        this.AggregateRepository, connection,
                        this.GuidGenerator);
                    activity.AssignPreviewImage(response.FileId);
                }
            }

            await this.AggregateRepository.SaveAsync(activityList);
            await this.AggregateRepository.SaveAsync(activity);
            return new CreatePercentageActivityResponse(activityId);
        }

        #endregion
    }
}