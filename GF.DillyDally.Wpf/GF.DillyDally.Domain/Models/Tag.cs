using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.Domain.Models
{
    internal sealed class Tag
    {
        #region Properties, Indexers

        public TagKey TagKey { get; }
        public string Name { get; }

        #endregion
    }
}