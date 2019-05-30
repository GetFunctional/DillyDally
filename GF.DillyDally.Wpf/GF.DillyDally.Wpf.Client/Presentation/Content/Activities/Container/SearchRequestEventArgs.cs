using System;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Container
{
    internal class SearchRequestEventArgs : EventArgs
    {
        public string SearchText { get; }

        public SearchRequestEventArgs(string searchText)
        {
            this.SearchText = searchText;
        }
    }
}