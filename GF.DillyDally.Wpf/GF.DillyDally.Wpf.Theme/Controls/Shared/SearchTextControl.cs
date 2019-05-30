using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Editors;

namespace GF.DillyDally.Wpf.Theme.Controls.Shared
{
    [TemplatePart(Name = PARTTextEdit, Type = typeof(TextEdit))]
    [DesignTimeVisible(true)]
    public class SearchTextControl : Control
    {
        public const string PARTTextEdit = "PART_TextEdit";

        public static readonly DependencyProperty InputValueProperty = DependencyProperty.Register(
            "InputValue", typeof(string), typeof(SearchTextControl), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            "Label", typeof(object), typeof(SearchTextControl), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty WatermarkTextProperty = DependencyProperty.Register(
            "WatermarkText", typeof(string), typeof(SearchTextControl), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty LastSearchItemsSourceProperty = DependencyProperty.Register(
            "LastSearchItemsSource", typeof(object), typeof(SearchTextControl), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty SearchResultItemsSourceProperty = DependencyProperty.Register(
            "SearchResultItemsSource", typeof(object), typeof(SearchTextControl),
            new PropertyMetadata(default(object)));

        public static readonly DependencyProperty SearchResultItemTemplateProperty = DependencyProperty.Register(
            "SearchResultItemTemplate", typeof(DataTemplate), typeof(SearchTextControl),
            new PropertyMetadata(default(DataTemplate)));

        public static readonly DependencyProperty SelectedResultProperty = DependencyProperty.Register(
            "SelectedResult", typeof(object), typeof(SearchTextControl), new PropertyMetadata(default(object)));

        private ComboBoxEdit _textEdit;

        static SearchTextControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(SearchTextControl), new FrameworkPropertyMetadata(typeof(SearchTextControl)));
        }

        public object SelectedResult
        {
            get { return this.GetValue(SelectedResultProperty); }
            set { this.SetValue(SelectedResultProperty, value); }
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
        public string InputValue
        {
            get { return (string) this.GetValue(InputValueProperty); }
            set { this.SetValue(InputValueProperty, value); }
        }

        [Bindable(true)]
        public object Label
        {
            get { return this.GetValue(LabelProperty); }
            set { this.SetValue(LabelProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this._textEdit = this.GetTemplateChild(PARTTextEdit) as ComboBoxEdit;
        }
    }
}