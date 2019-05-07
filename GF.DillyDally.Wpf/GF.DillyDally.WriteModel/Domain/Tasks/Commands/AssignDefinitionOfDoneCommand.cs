using System;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Commands
{
    public sealed class AssignDefinitionOfDoneCommand : IRequest<AssignDefinitionOfDoneResponse>
    {
        public AssignDefinitionOfDoneCommand(Guid taskId, string definitionOfDone)
        {
            this.TaskId = taskId;
            this.DefinitionOfDone = definitionOfDone;
        }

        public Guid TaskId { get; }
        public string DefinitionOfDone { get; }
    }
}