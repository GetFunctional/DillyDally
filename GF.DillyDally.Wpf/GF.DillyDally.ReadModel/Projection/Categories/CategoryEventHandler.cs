using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Repository;
using GF.DillyDally.ReadModel.Repository.Entities;
using GF.DillyDally.WriteModel.Domain.Categories.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.ReadModel.Projection.Categories
{
    internal sealed class CategoryEventHandler : IEventHandler<CategoryCreatedEvent>
    {
        private readonly DatabaseFileHandler _fileHandler;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryEventHandler(DatabaseFileHandler fileHandler, ICategoryRepository categoryRepository)
        {
            this._fileHandler = fileHandler;
            this._categoryRepository = categoryRepository;
        }

        #region IEventHandler<CategoryCreatedEvent> Members

        public void Handle(CategoryCreatedEvent @event)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                this._categoryRepository.InsertAsync(connection, new CategoryEntity()
                                                                 {
                                                                     CategoryId = @event.AggregateId,
                                                                     Name = @event.Name,
                                                                     ColorCode = @event.ColorCode,
                                                                     RunningNumberId = @event.RunningNumberId
                                                                 });
            }
        }

        #endregion
    }
}