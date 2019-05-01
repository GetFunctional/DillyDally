using System;

namespace GF.DillyDally.WriteModel.Domain.Tasks
{
    public class CreateTaskResponse
    {
        public CreateTaskResponse(Guid taskId)
        {
            this.TaskId = taskId;
        }

        public Guid TaskId { get; }
    }
}