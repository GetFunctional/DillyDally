using System;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Exceptions
{
    internal class DuplicateContributorException : Exception
    {
        public DuplicateContributorException(Guid achievementId) : base($"Duplicate Contributor {achievementId}")
        {
        }
    }
}