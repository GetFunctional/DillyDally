using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Projection.Categories.Repository;
using GF.DillyDally.WriteModel.Domain.Categories.Events;
using MediatR;

namespace GF.DillyDally.ReadModel.Projection.Categories
{
    internal sealed class CategoryEventHandler : INotificationHandler<CategoryCreatedEvent>
    {
        private readonly DatabaseFileHandler _fileHandler;

        public CategoryEventHandler(DatabaseFileHandler fileHandler)
        {
            this._fileHandler = fileHandler;
        }

        #region INotificationHandler<CategoryCreatedEvent> Members

        public async Task Handle(CategoryCreatedEvent notification, CancellationToken cancellationToken)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                var categoryRepository = new CategoryRepository();
                await categoryRepository.InsertAsync(connection, new CategoryEntity
                                                                 {
                                                                     CategoryId = notification.AggregateId,
                                                                     Name = notification.Name,
                                                                     ColorCode = notification.ColorCode,
                                                                     RunningNumberId = notification.RunningNumberId
                                                                 });
            }
        }

        #endregion
    }
}