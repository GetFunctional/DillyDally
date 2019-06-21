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
        IRequestHandler<CreatePercentageActivityCommand, CreatePercentageActivityResponse>
    {
        private readonly DatabaseFileHandler _databaseFileHandler;

        public ActivityCommandHandler(IAggregateRepository aggregateRepository, DatabaseFileHandler databaseFileHandler) : base(aggregateRepository)
        {
            this._databaseFileHandler = databaseFileHandler;
        }

        #region IRequestHandler<CreatePercentageActivityCommand,CreatePercentageActivityResponse> Members

        public async Task<CreatePercentageActivityResponse> Handle(CreatePercentageActivityCommand request,
            CancellationToken cancellationToken)
        {
            var activityId = this.GuidGenerator.GenerateGuid();
            var aggregate = ActivityAggregateRoot.Create(activityId, request.Name, ActivityType.Percentage);

            if (request.PreviewImageBytes != null)
            {
                using (var connection = this._databaseFileHandler.OpenConnection())
                {
                    var fileCreateCommand = new StoreFileCommand(request.PreviewImageBytes);
                    var response = await FileCommandHandler.GetOrCreateFileAsync(fileCreateCommand, this.AggregateRepository, connection,
                        this.GuidGenerator);
                    aggregate.AssignPreviewImage(response.FileId);
                }
            }

            await this.AggregateRepository.SaveAsync(aggregate);
            return new CreatePercentageActivityResponse(activityId);
        }

        #endregion
    }
}