﻿using System.Threading;
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
        IRequestHandler<CanCreateActivityCommand, CanCreateActivityResponse>, IRequestHandler<AttachActivityFieldCommand, AttachActivityFieldResponse>
    {
        private readonly IWriteModelStore _writeModelStore;

        public ActivityCommandHandler(IAggregateRepository aggregateRepository, IWriteModelStore writeModelStore)
            : base(aggregateRepository)
        {
            this._writeModelStore = writeModelStore;
        }

        #region IRequestHandler<AttachActivityFieldsCommand,AttachActivityFieldsResponse> Members

        public async Task<AttachActivityFieldResponse> Handle(AttachActivityFieldCommand request, CancellationToken cancellationToken)
        {
            var activity = this.AggregateRepository.GetById<ActivityAggregateRoot>(request.ActivityId);
            activity.AddActivityField(request.ActivityFieldType, request.FieldName, request.UnitOfMeasure);
            await this.AggregateRepository.SaveAsync(activity);
            return new AttachActivityFieldResponse();
        }

        #endregion

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
                using (var connection = this._writeModelStore.OpenConnection())
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