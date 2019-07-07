using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GF.DillyDally.Wpf.Theme.Controls.Layout
{
    [TemplatePart(Name = PARTHeader, Type = typeof(FormHeader))]
    [TemplatePart(Name = PARTFooter, Type = typeof(FormFooter))]
    [DesignTimeVisible(true)]
    public class Form : ContentControl
    {
        public const string PARTHeader = "PART_Header";
        public const string PARTFooter = "PART_Footer";

        public static readonly DependencyProperty FooterContentProperty = DependencyProperty.Register(
            "FooterContent", typeof(object), typeof(Form), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(Form), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            "Description", typeof(string), typeof(Form), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty CloseCommandProperty = DependencyProperty.Register(
            "CloseCommand", typeof(ICommand), typeof(Form),
            new PropertyMetadata(default(ICommand), HandleCloseCommandChanged));

        private FormFooter _footer;

        private FormHeader _header;

        static Form()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(Form), new FrameworkPropertyMetadata(typeof(Form)));
        }

        public ICommand CloseCommand
        {
            get { return (ICommand) this.GetValue(CloseCommandProperty); }
            set { this.SetValue(CloseCommandProperty, value); }
        }


        [Bindable(true)]
        public string Title
        {
            get { return (string) this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }

        [Bindable(true)]
        public string Description
        {
            get { return (string) this.GetValue(DescriptionProperty); }
            set { this.SetValue(DescriptionProperty, value); }
        }

        [Bindable(true)]
        public object FooterContent
        {
            get { return this.GetValue(FooterContentProperty); }
            set { this.SetValue(FooterContentProperty, value); }
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this._header = this.GetTemplateChild(PARTHeader) as FormHeader;
            this._footer = this.GetTemplateChild(PARTFooter) as FormFooter;

            this._header.CloseCommand = this.CloseCommand;
        }

        private static void HandleCloseCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var form = (Form) d;

            form.AssignCloseCommandToHeader((ICommand) e.NewValue);
        }

        private void AssignCloseCommandToHeader(ICommand command)
        {
            this._header.CloseCommand = command;
        }
    }
}