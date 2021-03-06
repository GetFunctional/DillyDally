using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Content.Category;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Create
{
    internal sealed class CreateTaskBasicInfosViewModel : DisplayPageViewModelBase
    {
        private CategorySelectorViewModel _categorySelectorViewModel;
        private byte[] _previewImageBytes;
        private string _taskName;

        public CreateTaskBasicInfosViewModel(CategorySelectorViewModel categorySelectorViewModel)
        {
            this._categorySelectorViewModel = categorySelectorViewModel;
        }


        public string TaskName
        {
            get { return this._taskName; }
            set { this.SetAndRaiseIfChanged(ref this._taskName, value); }
        }

        public CategoryViewModel SelectedCategory
        {
            get { return this._categorySelectorViewModel.SelectedCategory; }
        }

        public CategorySelectorViewModel CategorySelectorViewModel
        {
            get { return this._categorySelectorViewModel; }
            set { this.SetAndRaiseIfChanged(ref this._categorySelectorViewModel, value); }
        }

        public override string Title { get; } = "Task Infos";

        public byte[] PreviewImageBytes
        {
            get { return this._previewImageBytes; }
            set { this.SetAndRaiseIfChanged(ref this._previewImageBytes, value); }
        }
    }
}