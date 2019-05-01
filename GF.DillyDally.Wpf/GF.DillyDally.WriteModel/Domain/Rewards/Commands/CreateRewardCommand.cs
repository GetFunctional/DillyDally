using System;
using GF.DillyDally.WriteModel.Infrastructure;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Rewards.Commands
{
    public sealed class CreateRewardCommand : IRequest<CreateRewardResponse>
    {
        public CreateRewardCommand(string name, string currencyCode)
        {
            this.Name = name;
            this.CurrencyCode = currencyCode;
        }

        public string Name { get; }
        public string CurrencyCode { get; }
    }
}