using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using GF.DillyDally.Wpf.Theme.Extensions;

namespace GF.DillyDally.Wpf.Theme.Controls.Shared
{
    [TemplatePart(Name = PARTItemsDisplay, Type = typeof(ComboBoxEdit))]
    [TemplatePart(Name = PARTSearchBox, Type = typeof(TextEdit))]
    [DesignTimeVisible(true)]
    public class SearchTextControl : Control
    {
        public const string PARTSearchBox = "PART_SearchBox";
        public const string PARTItemsDisplay = "PART_ItemsDisplay";

        public static readonly DependencyProperty InputValueProperty = DependencyProperty.Register(
            "InputValue", typeof(object), typeof(SearchTextControl), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            "Label", typeof(object), typeof(SearchTextControl), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty WatermarkTextProperty = DependencyProperty.Register(
            "WatermarkText", typeof(string), typeof(SearchTextControl), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty LastSearchItemsSourceProperty = DependencyProperty.Register(
            "LastSearchItemsSource", typeof(object), typeof(SearchTextControl), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty SearchResultItemsSourceProperty = DependencyProperty.Register(
            "SearchResultItemsSource", typeof(object), typeof(SearchTextControl),
            new PropertyMetadata(default));

        public static readonly DependencyProperty SearchResultItemTemplateProperty = DependencyProperty.Register(
            "SearchResultItemTemplate", typeof(DataTemplate), typeof(SearchTextControl),
            new PropertyMetadata(default(DataTemplate)));

        public static readonly DependencyProperty SearchTextProperty = DependencyProperty.Register(
            "SearchText", typeof(string), typeof(SearchTextControl), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty DisplayMemberProperty = DependencyProperty.Register(
            "DisplayMember", typeof(string), typeof(SearchTextControl), new PropertyMetadata(default(string)));

        private ComboBoxEdit _itemsDisplay;
        private TextEdit _searchBox;

        private IObservable<EventArgs> _searchItemsSourceObservable;

        static SearchTextControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(SearchTextControl), new FrameworkPropertyMetadata(typeof(SearchTextControl)));
        }


        [Bindable(true)]
        public string SearchText
        {
            get { return (string) this.GetValue(SearchTextProperty); }
            set { this.SetValue(SearchTextProperty, value); }
        }

        public object LastSearchItemsSource
        {
            get { return this.GetValue(LastSearchItemsSourceProperty); }
            set { this.SetValue(LastSearchItemsSourceProperty, value); }
        }

        public object SearchResultItemsSource
        {
            get { return this.GetValue(SearchResultItemsSourceProperty); }
            set { this.SetValue(SearchResultItemsSourceProperty, value); }
        }

        public DataTemplate SearchResultItemTemplate
        {
            get { return (DataTemplate) this.GetValue(SearchResultItemTemplateProperty); }
            set { this.SetValue(SearchResultItemTemplateProperty, value); }
        }

        [Bindable(true)]
        public string WatermarkText
        {
            get { return (string) this.GetValue(WatermarkTextProperty); }
            set { this.SetValue(WatermarkTextProperty, value); }
        }

        [Bindable(true)]
        public object InputValue
        {
            get { return this.GetValue(InputValueProperty); }
            set { this.SetValue(InputValueProperty, value); }
        }

        [Bindable(true)]
        public object Label
        {
            get { return this.GetValue(LabelProperty); }
            set { this.SetValue(LabelProperty, value); }
        }

        public string DisplayMember
        {
            get { return (string) this.GetValue(DisplayMemberProperty); }
            set { this.SetValue(DisplayMemberProperty, value); }
        }

        public void HandleWhenPopupOpened(RoutedEventArgs e)
        {
            var popup = this._itemsDisplay.GetPopup();
            popup.PlacementTarget = this._searchBox;
            popup.Placement = PlacementMode.Bottom;
        }

        public void SearchResultItemsSourceChanged(EventArgs eventArgs)
        {
            if (this.Visibility == Visibility.Visible)
            {
                this._itemsDisplay.ShowPopup();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this._searchBox = (TextEdit) this.GetTemplateChild(PARTSearchBox);
            this._itemsDisplay = (ComboBoxEdit) this.GetTemplateChild(PARTItemsDisplay);

            this.EventRegistrations();
        }

        private void EventRegistrations()
        {
            var whenEditValueChanged = Observable
                .FromEventPattern<EditValueChangedEventHandler, EditValueChangedEventArgs>(
                    s => this._itemsDisplay.EditValueChanged += s,
                    s => this._itemsDisplay.EditValueChanged -= s)
                .Select(x => x.EventArgs);
            whenEditValueChanged.ObserveOnDispatcher()
                .Subscribe(this.SearchBoxOnEditValueChanged);

            var whenPopupOpened = Observable
                .FromEventPattern<RoutedEventHandler, RoutedEventArgs>(s => this._itemsDisplay.PopupOpened += s,
                    s => this._itemsDisplay.PopupOpened -= s)
                .Select(x => x.EventArgs);
            whenPopupOpened.ObserveOnDispatcher()
                .Subscribe(this.HandleWhenPopupOpened);

            this._searchItemsSourceObservable = this.Observe(SearchResultItemsSourceProperty);
            this._searchItemsSourceObservable.Throttle(TimeSpan.FromMilliseconds(25)).ObserveOnDispatcher()
                .Subscribe(this.SearchResultItemsSourceChanged);
        }

        private void SearchBoxOnEditValueChanged(EditValueChangedEventArgs e)
        {
            this.InputValue = e.NewValue;
            if (this.InputValue != null && !string.IsNullOrEmpty(this.DisplayMember))
            {
                var displayProperty = this.InputValue.GetType().GetProperty(this.DisplayMember);
                if (displayProperty != null)
                {
                    this.SearchText = displayProperty.GetValue(this.InputValue).ToString();
                }
            }
        }
    }
}