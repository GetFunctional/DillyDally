using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Domain.Activities.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Activities
{
    internal sealed class ActivityCommandHandler : CommandHandlerBase,
        IRequestHandler<CreatePercentageActivityCommand, CreatePercentageActivityResponse>
    {
        public ActivityCommandHandler(IAggregateRepository aggregateRepository) : base(aggregateRepository)
        {
        }

        #region IRequestHandler<CreatePercentageActivityCommand,CreatePercentageActivityResponse> Members

        public async Task<CreatePercentageActivityResponse> Handle(CreatePercentageActivityCommand request,
            CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                var activityId = this.GuidGenerator.GenerateGuid();

                var aggregate =
                    ActivityAggregateRoot.Create(activityId, request.Name, ActivityType.Percentage);
                this.AggregateRepository.Save(aggregate);
                return new CreatePercentageActivityResponse(activityId);
            }, cancellationToken);
        }

        #endregion
    }
}