using System;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.RunningNumbers
{
    internal sealed class RunningNumberFactory
    {
        private readonly IAggregateRepository _aggregateRepository;
        private readonly IGuidGenerator _guidGenerator;

        public RunningNumberFactory(IAggregateRepository aggregateRepository, IGuidGenerator guidGenerator)
        {
            this._aggregateRepository = aggregateRepository;
            this._guidGenerator = guidGenerator;
        }

        internal async Task<Guid> CreateNewRunningNumberForAsync(RunningNumberCounterArea area)
        {
            var runningNumberIdForTasks =
                RunningNumberCounterCommandHandler.AreaToIdentityMapping[area];
            var runningNumbers =
                this._aggregateRepository.GetById<RunningNumberCounterAggregateRoot>(runningNumberIdForTasks);
            var newRunningNumberId = this._guidGenerator.GenerateGuid();
            runningNumbers.AddNextNumber(newRunningNumberId);
            await this._aggregateRepository.SaveAsync(runningNumbers);

            return newRunningNumberId;
        }
    }
}