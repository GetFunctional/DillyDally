using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace GF.DillyDally.Wpf.Theme.Controls
{
    [TemplatePart(Name = PARTLinkControl, Type = typeof(TextBlock))]
    [DesignTimeVisible(true)]
    public class InternalLinkControl : Control
    {
        public const string PARTLinkControl = "PART_Link";

        public static readonly DependencyProperty DisplayLinkProperty = DependencyProperty.Register(
            "DisplayLink", typeof(string), typeof(InternalLinkControl), new PropertyMetadata(default(string)));

        private Hyperlink _linkControl;
        public IObservable<string> RunningNumberClicked;

        static InternalLinkControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(InternalLinkControl), new FrameworkPropertyMetadata(typeof(InternalLinkControl)));
        }

        public string DisplayLink
        {
            get { return (string) this.GetValue(DisplayLinkProperty); }
            set { this.SetValue(DisplayLinkProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this._linkControl = this.GetTemplateChild(PARTLinkControl) as Hyperlink;

            if (this._linkControl != null)
            {
                this.RunningNumberClicked = Observable
                    .FromEventPattern<RequestNavigateEventHandler, RequestNavigateEventArgs>(
                        h => this._linkControl.RequestNavigate += h,
                        h => this._linkControl.RequestNavigate -= h).Select(args => args.EventArgs.Target);
            }
        }
    }
}