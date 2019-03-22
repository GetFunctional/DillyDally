using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.Domain.Models
{
    internal sealed class Tag
    {
        public TagKey TagKey { get; }
        public string Name { get; }
    }
}