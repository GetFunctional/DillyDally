﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Theme.Controls
{
    [DesignTimeVisible(true)]
    public class TextInputControl : Control
    {
        public static readonly DependencyProperty InputValueProperty = DependencyProperty.Register(
            "InputValue", typeof(string), typeof(TextInputControl), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            "Label", typeof(object), typeof(TextInputControl), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty WatermarkTextProperty = DependencyProperty.Register(
            "WatermarkText", typeof(string), typeof(TextInputControl), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty IsMultilineProperty = DependencyProperty.Register(
            "IsMultiline", typeof(bool), typeof(TextInputControl), new PropertyMetadata(default(bool)));

        static TextInputControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(TextInputControl), new FrameworkPropertyMetadata(typeof(TextInputControl)));
        }

        public bool IsMultiline
        {
            get { return (bool) this.GetValue(IsMultilineProperty); }
            set { this.SetValue(IsMultilineProperty, value); }
        }

        public string WatermarkText
        {
            get { return (string) this.GetValue(WatermarkTextProperty); }
            set { this.SetValue(WatermarkTextProperty, value); }
        }

        public string InputValue
        {
            get { return (string) this.GetValue(InputValueProperty); }
            set { this.SetValue(InputValueProperty, value); }
        }

        public object Label
        {
            get { return this.GetValue(LabelProperty); }
            set { this.SetValue(LabelProperty, value); }
        }
    }
}