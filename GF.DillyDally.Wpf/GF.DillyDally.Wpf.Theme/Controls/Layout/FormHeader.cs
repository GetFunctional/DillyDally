using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Mvvm.Native;

namespace GF.DillyDally.Wpf.Theme.Controls.Layout
{
    [DesignTimeVisible(true)]
    public class FormHeader : Control
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(FormHeader), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            "Description", typeof(string), typeof(FormHeader), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty CloseCommandProperty = DependencyProperty.Register(
            "CloseCommand", typeof(ICommand), typeof(FormHeader),
            new PropertyMetadata(default(ICommand), HandleCloseCommandChanged));

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
        public ICommand CloseCommand
        {
            get { return (ICommand) this.GetValue(CloseCommandProperty); }
            set { this.SetValue(CloseCommandProperty, value); }
        }

        private static void HandleCloseCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var form = (FormHeader) d;
            var newCommand = (ICommand) e.NewValue;
            var oldCommand = (ICommand) e.OldValue;

            if (oldCommand != null)
            {
                form.RemoveCommandBindingWith(oldCommand);
            }

            if (newCommand != null)
            {
                form.AddCommandBindingWith(newCommand);
            }
        }

        private void AddCommandBindingWith(ICommand command)
        {
            var bindings = new HashSet<CommandBinding>();
            bindings.Add(new CommandBinding(command));

            foreach (CommandBinding commandBinding in this.CommandBindings)
            {
                if (commandBinding.Command == command)
                {
                    bindings.Remove(commandBinding);
                }
            }

            bindings.ForEach(b => this.CommandBindings.Add(b));
        }

        private void RemoveCommandBindingWith(ICommand command)
        {
            var bindings = new HashSet<CommandBinding>();
            foreach (CommandBinding commandBinding in this.CommandBindings)
            {
                if (commandBinding.Command == command)
                {
                    bindings.Add(commandBinding);
                }
            }

            bindings.ForEach(b => this.CommandBindings.Remove(b));
        }
    }
}