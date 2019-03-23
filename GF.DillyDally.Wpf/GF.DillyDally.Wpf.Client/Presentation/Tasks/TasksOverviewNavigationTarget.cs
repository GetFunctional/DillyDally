using System;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using GF.DillyDally.Wpf.Client.Presentation.Tasks;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    public sealed class TasksOverviewNavigationTarget : INavigationTarget
    {
        public TasksOverviewNavigationTarget()
        {
            this.NavigationTargetId = Guid.Parse("{8F0D2BD9-3B9E-4E5D-A786-111A2FEAB664}");
            this.DisplayName = "Tasks overview";
            this.NavigationTargetControllerType = typeof(TasksOverviewController);
        }

        #region INavigationTarget Members

        public bool Equals(INavigationTarget other)
        {
            if (other is TasksOverviewNavigationTarget otherTarget)
            {
                return this.Equals(otherTarget);
            }

            return false;
        }

        public Guid NavigationTargetId { get; }
        public string DisplayName { get; }
        public Type NavigationTargetControllerType { get; }

        #endregion

        private bool Equals(TasksOverviewNavigationTarget other)
        {
            return Equals(this.NavigationTargetId, other.NavigationTargetId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is TasksOverviewNavigationTarget other && this.Equals(other);
        }

        public override int GetHashCode()
        {
            return this.NavigationTargetId.GetHashCode();
        }
    }
}