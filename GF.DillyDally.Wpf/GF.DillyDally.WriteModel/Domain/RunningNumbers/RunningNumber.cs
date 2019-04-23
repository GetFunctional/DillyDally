using System;

namespace GF.DillyDally.WriteModel.Domain.RunningNumbers
{
    internal sealed class RunningNumber
    {
        public RunningNumber(Guid runningNumberId, string prefix, int number)
        {
            this.RunningNumberId = runningNumberId;
            this.Prefix = prefix;
            this.Number = number;
        }

        public Guid RunningNumberId { get; }

        public string Prefix { get; }

        public int Number { get; }
    }
}