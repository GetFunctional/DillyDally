﻿using GF.DillyDally.WriteModel.Domain.Categories.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Categories
{
    internal sealed class CategoryEventHandler : IEventHandler<CategoryCreatedEvent>
    {
        #region IEventHandler<CategoryCreatedEvent> Members

        public void Handle(CategoryCreatedEvent @event)
        {
        }

        #endregion
    }
}