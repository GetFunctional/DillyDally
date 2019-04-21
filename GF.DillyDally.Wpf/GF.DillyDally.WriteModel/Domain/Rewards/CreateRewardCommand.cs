using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Rewards
{
    public sealed class CreateRewardCommand : AggregateCommandBase
    {
        public CreateRewardCommand(string name, string currencyCode) : base(Guid.Empty)
        {
            this.Name = name;
            this.CurrencyCode = currencyCode;
        }

        public string Name { get; }
        public string CurrencyCode { get; }
    }
}