using System;

namespace GF.DillyDally.WriteModel.Domain.Lanes
{
    internal sealed class InvalidColorCodeGivenException : Exception
    {
        public InvalidColorCodeGivenException(string colorCode) : base($"Invalid Color {colorCode}")
        {
        }
    }
}