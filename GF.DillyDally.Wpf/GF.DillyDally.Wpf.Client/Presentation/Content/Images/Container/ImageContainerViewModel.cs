using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Images.Container
{
    public class ImageContainerViewModel : ViewModelBase, IDisposable
    {
        private IList<ImageContainerItemViewModel> _images;
        private readonly Subject<IList<ImageContainerItemViewModel>> _imagesChangedSubject = new Subject<IList<ImageContainerItemViewModel>>();

        public ImageContainerViewModel()
        {
            this._images = new List<ImageContainerItemViewModel>();
        }

        internal IObservable<IList<ImageContainerItemViewModel>> WhenImageCollectionChanged
        {
            get { return this._imagesChangedSubject; }
        }

        public IList<ImageContainerItemViewModel> Images
        {
            get
            {
                return this._images;
            }
            private set
            {
                if (this.SetAndRaiseIfChanged(ref this._images, value))
                {
                    this._imagesChangedSubject.OnNext(value);
                }
            }
        }

        internal void AddImages(IEnumerable<ImageContainerItemViewModel> newImages)
        {
            var images = this.Images.Concat(newImages).Distinct().ToList();
            this.Images = images;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                this._imagesChangedSubject?.Dispose();
            }
        }
    }
}