using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GF.DillyDally.Wpf.Theme.Controls
{
    [DesignTimeVisible(true)]
    public class LabelControl : Control
    {
        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
            "LabelText", typeof(string), typeof(LabelControl), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty LabelBrushProperty = DependencyProperty.Register(
            "LabelBrush", typeof(Brush), typeof(LabelControl), new PropertyMetadata(default(Brush)));

        static LabelControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(LabelControl), new FrameworkPropertyMetadata(typeof(LabelControl)));
        }

        public Brush LabelBrush
        {
            get
            {
                return (Brush)this.GetValue(LabelBrushProperty);
            }
            set
            {
                this.SetValue(LabelBrushProperty, value);
            }
        }

        public string LabelText
        {
            get
            {
                return (string)this.GetValue(LabelTextProperty);
            }
            set
            {
                this.SetValue(LabelTextProperty, value);
            }
        }
    }
}