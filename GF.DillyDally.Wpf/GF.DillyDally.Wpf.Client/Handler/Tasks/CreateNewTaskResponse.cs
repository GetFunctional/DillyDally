using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Wpf.Client.Handler.Tasks
{
    public sealed class CreateNewTaskResponse
    {
        public CreateNewTaskResponse(TaskKey newTaskId, bool processCanceled = false)
        {
            this.NewTaskId = newTaskId;
            this.ProcessCanceled = processCanceled;
        }

        public bool ProcessCanceled { get; }
        public TaskKey NewTaskId { get; }

        public static CreateNewTaskResponse Canceled()
        {
            return new CreateNewTaskResponse(null, true);
        }
    }
}