using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create
{
    /// <summary>
    ///     Interaktionslogik für ShowCaseView.xaml
    /// </summary>
    public partial class CreateActivityView : IViewFor<CreateActivityViewModel>
    {
        public CreateActivityView()
        {
            this.InitializeComponent();
        }

        #region IViewFor<CreateActivityViewModel> Members

        public CreateActivityViewModel ViewModel
        {
            get { return (CreateActivityViewModel) this.DataContext; }
        }

        #endregion
    }
}