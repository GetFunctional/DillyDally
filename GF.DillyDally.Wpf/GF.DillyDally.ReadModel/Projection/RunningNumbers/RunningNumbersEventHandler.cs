using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Projection.RunningNumbers.Repository;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using MediatR;

namespace GF.DillyDally.ReadModel.Projection.RunningNumbers
{
    internal sealed class RunningNumbersEventHandler : INotificationHandler<RunningNumberCounterCreatedEvent>,
        INotificationHandler<AddNextNumberEvent>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public RunningNumbersEventHandler(IDbConnectionFactory dbConnectionFactory)
        {
            this._dbConnectionFactory = dbConnectionFactory;
        }

        #region INotificationHandler<AddNextNumberEvent> Members

        public async Task Handle(AddNextNumberEvent notification, CancellationToken cancellationToken)
        {
            using (var connection = this._dbConnectionFactory.OpenConnection())
            {
                var runningNumberCounterRepository = new RunningNumberCounterRepository();
                var runningNumberCounterId = notification.AggregateId;
                var runningNumberRepository = new RunningNumberRepository();

                var runningNumber = $"{notification.Prefix}-{notification.NextNumberInRow}";
                await runningNumberRepository.CreateNewRunningNumberAsync(connection, notification.NextNumberId,
                    runningNumberCounterId, runningNumber);
                await runningNumberCounterRepository.IncreaseCounterAsync(connection, runningNumberCounterId);
            }
        }

        #endregion

        #region INotificationHandler<RunningNumberCounterCreatedEvent> Members

        public async Task Handle(RunningNumberCounterCreatedEvent notification, CancellationToken cancellationToken)
        {
            using (var connection = this._dbConnectionFactory.OpenConnection())
            {
                var repository = new RunningNumberCounterRepository();
                var runningNumberCounterId = notification.AggregateId;
                var counterArea = notification.CounterArea;
                var initialNumber = notification.InitialNumber;
                var prefix = notification.Prefix;

                await repository.CreateNewRunningNumberCounterAsync(connection, runningNumberCounterId, counterArea,
                    initialNumber, prefix);
            }
        }

        #endregion
    }
}