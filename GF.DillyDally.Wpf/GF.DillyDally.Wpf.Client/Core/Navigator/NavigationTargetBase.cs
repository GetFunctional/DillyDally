using System;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    public abstract class NavigationTargetBase<TController> : INavigationTarget where TController : IController
    {
        #region INavigationTarget Members

        public Guid NavigationTargetId { get; protected set; }
        public string DisplayName { get; protected set; }

        public Type NavigationTargetControllerType
        {
            get
            {
                return typeof(TController);
            }
        }


        public virtual bool Equals(INavigationTarget other)
        {
            if (other == null)
            {
                return false;
            }

            return this.NavigationTargetId == other.NavigationTargetId;
        }

        #endregion


        public override int GetHashCode()
        {
            return this.NavigationTargetId.GetHashCode();
        }
    }
}