using System.ComponentModel;

namespace GF.DillyDally.Wpf.Theme.Controls.Layout
{
    public interface ITabItem : INotifyPropertyChanged
    {
        string Title { get; set; }

        bool IsSelected { get; set; }

        INotifyPropertyChanged Content { get; }
    }
}