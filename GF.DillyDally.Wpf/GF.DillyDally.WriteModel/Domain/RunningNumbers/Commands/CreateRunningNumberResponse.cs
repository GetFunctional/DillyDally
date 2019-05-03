using System;

namespace GF.DillyDally.WriteModel.Domain.RunningNumbers.Commands
{
    public class CreateRunningNumberResponse
    {
        public CreateRunningNumberResponse(Guid runningNumberId)
        {
            this.RunningNumberId = runningNumberId;
        }

        public Guid RunningNumberId { get; }
    }
}