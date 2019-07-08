using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Images.Container
{
    /// <summary>
    ///     Interaktionslogik für ImageContainerView.xaml
    /// </summary>
    public partial class ImageContainerView : IViewFor<ImageContainerViewModel>
    {
        public ImageContainerView()
        {
            this.InitializeComponent();
        }

        #region IViewFor<ImageContainerViewModel> Members

        public ImageContainerViewModel ViewModel
        {
            get
            {
                return (ImageContainerViewModel)this.DataContext;
            }
        }

        #endregion
    }
}