using System;
using System.ComponentModel;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Theme.Controls.Layout
{
    public class TabItem : ObservableObject, ITabItem
    {
        private string _badgeText;
        private bool _isSelected;
        private string _title;


        public TabItem(INotifyPropertyChanged content) : this(content, "Title")
        {
        }

        public TabItem(INotifyPropertyChanged content, string title) : this(content, title, delegate { return null;})
        {
        }

        public TabItem(INotifyPropertyChanged content, string title, Func<string> refreshBadgeTextFunction)
        {
            this.Title = title;
            this.RefreshBadgeTextFunction = refreshBadgeTextFunction;
            this.Content = content;
        }

        #region ITabItem Members

        public string Title
        {
            get { return this._title; }
            set { this.SetAndRaiseIfChanged(ref this._title, value); }
        }

        private Func<string> RefreshBadgeTextFunction { get; }

        public bool IsSelected
        {
            get { return this._isSelected; }
            set { this.SetAndRaiseIfChanged(ref this._isSelected, value); }
        }

        public INotifyPropertyChanged Content { get; }

        public string BadgeText
        {
            get { return this._badgeText; }
            set { this.SetAndRaiseIfChanged(ref this._badgeText, value); }
        }

        public void RefreshBadgeText()
        {
            this.BadgeText = this.RefreshBadgeTextFunction();
        }

        #endregion
    }
}