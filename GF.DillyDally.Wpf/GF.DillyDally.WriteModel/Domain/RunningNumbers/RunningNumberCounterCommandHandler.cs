using System;
using System.Collections.Generic;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Commands;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Exceptions;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.RunningNumbers
{
    internal sealed class RunningNumberCounterCommandHandler : CommandHandlerBase, ICommandHandler<CreateRunningNumberCounterCommand>
    {
        private readonly IAggregateRepository _aggregateRepository;

        internal static IDictionary<RunningNumberCounterArea, Guid> AreaToIdentityMapping =
            new Dictionary<RunningNumberCounterArea, Guid>()
            {
                { RunningNumberCounterArea.Task, Guid.Parse("{885D3926-FBAB-4EE9-823B-756BA812275D}")},
                { RunningNumberCounterArea.Category, Guid.Parse("{BE434835-ACAA-4CD6-87E0-F7EA338CD23D}")},
                { RunningNumberCounterArea.Lane, Guid.Parse("{E8652CB7-2908-48CA-882D-DF989FE4957F}")},
            };

        public RunningNumberCounterCommandHandler(IAggregateRepository aggregateRepository)
        {
            this._aggregateRepository = aggregateRepository;
        }

        #region ICommandHandler<CreateRunningNumberCounterCommand> Members

        public IAggregateRoot Handle(CreateRunningNumberCounterCommand counterCommand)
        {
            var runningNumberId = AreaToIdentityMapping[counterCommand.CounterArea];
            if (this._aggregateRepository.TryGetById(runningNumberId, out RunningNumberCounterAggregateRoot root))
            {
                throw new RunningNumberCounterAlreadyExistException(runningNumberId);
            }

            var aggregate =  RunningNumberCounterAggregateRoot.Create(runningNumberId, counterCommand.CounterArea, counterCommand.Prefix,
                counterCommand.InitialNumber);
            this._aggregateRepository.Save(aggregate);
            return aggregate;
        }

        #endregion
    }
}