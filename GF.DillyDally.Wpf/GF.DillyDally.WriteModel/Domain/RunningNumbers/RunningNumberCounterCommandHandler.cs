using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Commands;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Exceptions;
using GF.DillyDally.WriteModel.Infrastructure;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.RunningNumbers
{
    internal sealed class RunningNumberCounterCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateRunningNumberCounterCommand, CreateRunningNumberCounterResponse>,
        IRequestHandler<CreateRunningNumberCommand, CreateRunningNumberResponse>
    {
        internal static IDictionary<RunningNumberCounterArea, Guid> AreaToIdentityMapping =
            new Dictionary<RunningNumberCounterArea, Guid>
            {
                {RunningNumberCounterArea.Task, Guid.Parse("{885D3926-FBAB-4EE9-823B-756BA812275D}")},
                {RunningNumberCounterArea.Category, Guid.Parse("{BE434835-ACAA-4CD6-87E0-F7EA338CD23D}")},
                {RunningNumberCounterArea.Lane, Guid.Parse("{E8652CB7-2908-48CA-882D-DF989FE4957F}")},
                {RunningNumberCounterArea.Achievement, Guid.Parse("{C9ECE9A5-1B1D-4B9F-85F1-19A80B892AE3}")}
            };

        public RunningNumberCounterCommandHandler(IAggregateRepository aggregateRepository) : base(aggregateRepository)
        {
        }

        #region IRequestHandler<CreateRunningNumberCommand,CreateRunningNumberResponse> Members

        public async Task<CreateRunningNumberResponse> Handle(CreateRunningNumberCommand request,
            CancellationToken cancellationToken)
        {
            var runningNumberCounterId = AreaToIdentityMapping[request.CounterArea];
            var aggregate = this.AggregateRepository.GetById<RunningNumberCounterAggregateRoot>(runningNumberCounterId);
            var nextNumberId = this.GuidGenerator.GenerateGuid();
            aggregate.AddNextNumber(nextNumberId);
            await this.AggregateRepository.SaveAsync(aggregate);
            return new CreateRunningNumberResponse(nextNumberId);
        }

        #endregion

        #region IRequestHandler<CreateRunningNumberCounterCommand,CreateRunningNumberCounterResponse> Members

        public async Task<CreateRunningNumberCounterResponse> Handle(CreateRunningNumberCounterCommand request,
            CancellationToken cancellationToken)
        {
            var runningNumberId = AreaToIdentityMapping[request.CounterArea];
            if (this.AggregateRepository.TryGetById(runningNumberId, out RunningNumberCounterAggregateRoot root))
            {
                throw new RunningNumberCounterAlreadyExistException(runningNumberId);
            }

            var aggregate = RunningNumberCounterAggregateRoot.Create(runningNumberId, request.CounterArea,
                request.Prefix,
                request.InitialNumber);
            await this.AggregateRepository.SaveAsync(aggregate);
            return new CreateRunningNumberCounterResponse();
        }

        #endregion
    }
}