using System;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Commands
{
    internal sealed class ChangeTaskLaneCommand : IRequest<ChangeTaskLaneResponse>
    {
        internal ChangeTaskLaneCommand(Guid taskId, Guid destinationLaneId, Guid sourceLaneId)
        {
            this.TaskId = taskId;
            this.DestinationLaneId = destinationLaneId;
            this.SourceLaneId = sourceLaneId;
        }

        public Guid TaskId { get; }
        public Guid DestinationLaneId { get; }
        public Guid SourceLaneId { get; }
    }
}