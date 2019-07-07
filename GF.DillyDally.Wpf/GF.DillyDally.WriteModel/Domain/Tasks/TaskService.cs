using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Domain.Files;
using GF.DillyDally.WriteModel.Domain.Files.Commands;
using GF.DillyDally.WriteModel.Domain.Tasks.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Tasks
{
    public sealed class TaskService : IDomainService
    {
        private readonly IMediator _commandDispatcher;

        public TaskService(IMediator commandDispatcher)
        {
            this._commandDispatcher = commandDispatcher;
        }

        public async Task<AssignPreviewImageResponse> AttachPreviewImageToTaskAsync(Guid taskId, string filePath)
        {
            var fileCreateCommand = new StoreFileCommand(filePath);
            return await this.AttachPreviewImageToTaskAsync(taskId, fileCreateCommand);
        }
        private async Task<AssignPreviewImageResponse> AttachPreviewImageToTaskAsync(Guid taskId, StoreFileCommand fileCreateCommand)
        {
            var createdFile = await this._commandDispatcher.Send(fileCreateCommand);
            var attachedFile = await this.AttachFileToTaskAsync(taskId, createdFile.FileId);
            return await this.AssignPreviewImageToTaskAsync(taskId, attachedFile.FileId);
        }

        public async Task<AssignPreviewImageResponse> AttachPreviewImageToTaskAsync(Guid taskId, byte[] previewImage)
        {
            var fileCreateCommand = new StoreFileCommand(previewImage);
            return await this.AttachPreviewImageToTaskAsync(taskId, fileCreateCommand);
        }

        private async Task<AssignPreviewImageResponse> AssignPreviewImageToTaskAsync(Guid taskId, Guid fileId)
        {
            var fileCreateCommand = new AssignPreviewImageCommand(taskId, fileId);
            var assignedPreviewImage = await this._commandDispatcher.Send(fileCreateCommand);
            return assignedPreviewImage;
        }

        public async Task<AttachFileToTaskResponse> AttachFileToTaskAsync(Guid taskId, Guid fileId)
        {
            var fileCreateCommand = new AttachFileToTaskCommand(taskId, fileId);
            var attachedFile = await this._commandDispatcher.Send(fileCreateCommand);
            return attachedFile;
        }

        public async Task<CreateTaskResponse> CreateNewTaskAsync(string taskName, Guid categoryId, Guid? laneId)
        {
            var task = await this._commandDispatcher.Send(new CreateTaskCommand(taskName, categoryId, laneId));
            return task;
        }

        public async Task<ChangeTaskLaneResponse> ChangeTaskLaneAsync(Guid taskId, Guid targetLaneId, Guid sourceLaneId)
        {
            var task = await this._commandDispatcher.Send(new ChangeTaskLaneCommand(taskId, targetLaneId,
                sourceLaneId));
            return task;
        }

        public async Task<LinkTaskToActivitiesResponse> LinkTaskToActivitiesAsync(Guid taskId, ISet<Guid> activityIds)
        {
            var task = await this._commandDispatcher.Send(new LinkTaskToActivitiesCommand(taskId, activityIds));
            return task;
        }
    }
}