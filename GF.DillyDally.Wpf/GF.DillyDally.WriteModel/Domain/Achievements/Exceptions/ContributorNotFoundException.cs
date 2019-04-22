using System;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Exceptions
{
    internal class ContributorNotFoundException : Exception
    {
        public ContributorNotFoundException(Guid achievementIdToDetach) : base(
            $"Contributor not found {achievementIdToDetach}")
        {
        }
    }
}