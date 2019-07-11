using System.ComponentModel;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Theme.Controls.Layout
{
    public class TabItem : ObservableObject, ITabItem
    {
        private bool _isSelected;
        private string _title;


        public TabItem(INotifyPropertyChanged content) : this(content, "Title")
        {
        }

        public TabItem(INotifyPropertyChanged content, string title)
        {
            this.Title = title;
            this.Content = content;
        }

        #region ITabItem Members

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this.SetAndRaiseIfChanged(ref this._title, value);
            }
        }

        public bool IsSelected
        {
            get
            {
                return this._isSelected;
            }
            set
            {
                this.SetAndRaiseIfChanged(ref this._isSelected, value);
            }
        }

        public INotifyPropertyChanged Content { get; }

        #endregion
    }
}