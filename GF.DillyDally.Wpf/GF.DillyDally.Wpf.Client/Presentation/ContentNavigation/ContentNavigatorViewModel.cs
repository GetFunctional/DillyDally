using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Mvvmc.Contracts;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    public sealed class ContentNavigatorViewModel : ViewModelBase
    {
        private readonly ObservableCollection<IViewModel> _activeNavigationTargets;

        private string _contentTitle;

        private IViewModel _displayTarget;

        public ContentNavigatorViewModel()
        {
            this._activeNavigationTargets = new ObservableCollection<IViewModel>();
        }

        public IViewModel DisplayTarget
        {
            get
            {
                return this._displayTarget;
            }
            private set
            {
                this.SetAndRaiseIfChanged(ref this._displayTarget, value);
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
                this.SetAndRaiseIfChanged(ref this._contentTitle, value);
            }
        }

        public IReadOnlyList<IViewModel> ActiveNavigationTargets
        {
            get
            {
                return this._activeNavigationTargets;
            }
        }

        internal void AssignDisplayTarget(IViewModel displayTarget, string contentTitle)
        {
            var itemBefore = this._activeNavigationTargets.FirstOrDefault();

            if (this._activeNavigationTargets.Count > 1)
            {
                throw new ApplicationException();
            }

            this._activeNavigationTargets.Add(displayTarget);
            this.DisplayTarget = displayTarget;
            this.ContentTitle = contentTitle;

            // TODO NavigationHistory?
            if (itemBefore != null)
            {
                this._activeNavigationTargets.Remove(itemBefore);
            }
        }
    }
}