using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Wpf.Client.Handler.Tasks;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    internal sealed class CreateNewTaskRequest : IRequest<CreateNewTaskResponse>
    {
        public CreateNewTaskRequest(string initialName, TaskType taskType)
        {
            this.TaskType = taskType;
            this.InitialName = initialName;
        }

        public TaskType TaskType { get; }

        public string InitialName { get; }
    }
}