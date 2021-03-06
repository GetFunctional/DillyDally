﻿namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    /// <summary>
    ///     An entry in an INavigatorJournalEntry representing target.
    /// </summary>
    public interface INavigationJournalEntry
    {
        /// <summary>
        ///     Target of the journalentry.
        /// </summary>
        INavigationTarget NavigationTarget { get; }
    }
}