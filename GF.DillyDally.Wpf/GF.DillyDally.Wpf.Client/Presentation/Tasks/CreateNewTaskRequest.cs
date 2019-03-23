using GF.DillyDally.Data.Tasks;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    internal sealed class CreateNewTaskRequest : IRequest<Task>
    {
        public CreateNewTaskRequest(string initialName)
        {
            this.InitialName = initialName;
        }

        public string InitialName { get; }
    }
}