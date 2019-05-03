using System;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Commands
{
    public sealed class LinkTaskCommand : IRequest<LinkTaskResponse>
    {
        public LinkTaskCommand(Guid taskId, Guid linkToTaskId)
        {
            this.TaskId = taskId;
            this.LinkToTaskId = linkToTaskId;
        }

        public Guid TaskId { get; }
        public Guid LinkToTaskId { get; }
    }
}