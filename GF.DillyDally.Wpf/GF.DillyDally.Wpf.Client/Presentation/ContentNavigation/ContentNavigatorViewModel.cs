﻿using GF.DillyDally.Mvvmc;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    public sealed class ContentNavigatorViewModel : ViewModelBase
    {
        private string _contentTitle;

        private IViewModel _displayTarget;

        public IViewModel DisplayTarget
        {
            get
            {
                return this._displayTarget;
            }
            private set
            {
                this.RaiseAndSetIfChanged(ref this._displayTarget, value);
            }
        }

        public string ContentTitle
        {
            get
            {
                return this._contentTitle;
            }
            private set
            {
                this.RaiseAndSetIfChanged(ref this._contentTitle, value);
            }
        }

        internal void AssignDisplayTarget(IViewModel displayTarget, string contentTitle)
        {
            this.DisplayTarget = displayTarget;
            this.ContentTitle = contentTitle;
        }
    }
}