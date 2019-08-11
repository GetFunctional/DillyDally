using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.DillyDally.WriteModel.Games.Aggregates.Shelves
{
    internal class ShelfFactory
    {
        internal ShelfAggregate CreateShelf(Guid shelfId, string name)
        {
            return new ShelfAggregate(shelfId,name);
        }
    }
}
