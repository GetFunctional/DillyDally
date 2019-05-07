using System;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Exceptions
{
    internal class InvalidDefinitionOfDoneException : Exception
    {
        public InvalidDefinitionOfDoneException(string definitionOfDone) : base($"Invalid definition: {definitionOfDone}")
        {
            
        }
    }
}