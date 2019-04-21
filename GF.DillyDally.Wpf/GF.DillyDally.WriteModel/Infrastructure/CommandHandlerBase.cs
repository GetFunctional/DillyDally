using GF.DillyDally.Data.Sqlite;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal abstract class CommandHandlerBase
    {
        protected CommandHandlerBase()
        {
            
        }

        protected IGuidGenerator GuidGenerator { get; } = new GuidGenerator();
    }
}