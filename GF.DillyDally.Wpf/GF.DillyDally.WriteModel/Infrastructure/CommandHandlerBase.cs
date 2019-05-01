using GF.DillyDally.Data.Sqlite;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal abstract class CommandHandlerBase
    {
        protected CommandHandlerBase(IAggregateRepository aggregateRepository) : this(aggregateRepository,
            new GuidGenerator())
        {
        }

        protected CommandHandlerBase(IAggregateRepository aggregateRepository, IGuidGenerator guidGenerator)
        {
            this.AggregateRepository = aggregateRepository;
            this.GuidGenerator = guidGenerator;
        }

        protected IAggregateRepository AggregateRepository { get; }

        protected IGuidGenerator GuidGenerator { get; }
    }
}