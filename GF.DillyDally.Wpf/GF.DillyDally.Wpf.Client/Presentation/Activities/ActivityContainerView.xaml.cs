using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation.Activities
{
    /// <summary>
    ///     Interaktionslogik für AchievementContainerView.xaml
    /// </summary>
    public partial class ActivityContainerView : IViewFor<ActivityContainerViewModel>
    {
        public ActivityContainerView()
        {
            this.InitializeComponent();
        }

        #region IViewFor<ActivityContainerViewModel> Members

        public ActivityContainerViewModel ViewModel
        {
            get
            {
                return (ActivityContainerViewModel)this.DataContext;
            }
        }

        #endregion
    }
}