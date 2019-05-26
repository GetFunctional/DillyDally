using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Theme.Controls.Layout
{
    public interface IDisplayPage : IViewModel
    {
        string Title { get; }

        bool IsCurrent { get; set; }

        int PageNumber { get; set; }
    }
}