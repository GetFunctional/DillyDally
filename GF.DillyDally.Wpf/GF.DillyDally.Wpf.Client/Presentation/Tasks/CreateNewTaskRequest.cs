using GF.DillyDally.ReadModel.Tasks;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    internal sealed class CreateNewTaskRequest : IRequest<TaskEntity>
    {
        public CreateNewTaskRequest(string initialName)
        {
            this.InitialName = initialName;
        }

        public string InitialName { get; }
    }
}