using System;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Container
{
    internal class SearchRequestEventArgs : EventArgs
    {
        public SearchRequestEventArgs(string searchText)
        {
            this.SearchText = searchText;
        }

        public string SearchText { get; }
    }
}