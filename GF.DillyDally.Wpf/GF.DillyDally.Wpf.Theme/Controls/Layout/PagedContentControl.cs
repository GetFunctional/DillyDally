﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Mvvm;

namespace GF.DillyDally.Wpf.Theme.Controls.Layout
{
    public class PagedContentControl : ItemsControl
    {
        public static readonly DependencyProperty CurrentPageProperty = DependencyProperty.Register(
            "CurrentPage", typeof(object), typeof(PagedContentControl),
            new PropertyMetadata(default, HandleCurrentPageChanged));

        public static readonly DependencyProperty HasNextPageProperty = DependencyProperty.Register(
            "HasNextPage", typeof(bool), typeof(PagedContentControl), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty HasPreviousPageProperty = DependencyProperty.Register(
            "HasPreviousPage", typeof(bool), typeof(PagedContentControl), new PropertyMetadata(default(bool)));

        private readonly DelegateCommand _nextPageCommand;
        private readonly DelegateCommand _previousPageCommand;

        static PagedContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PagedContentControl),
                new FrameworkPropertyMetadata(typeof(PagedContentControl)));
        }


        public PagedContentControl()
        {
            this._nextPageCommand = new DelegateCommand(this.ExecuteSelectNextPage, this.CanExecuteSelectNextPage);
            this._previousPageCommand =
                new DelegateCommand(this.ExecuteSelectPreviousPage, this.CanExecuteSelectPreviousPage);
            this.CommandBindings.Add(new CommandBinding(this.NextPageCommand));
            this.CommandBindings.Add(new CommandBinding(this.PreviousPageCommand));
        }

        public ICommand PreviousPageCommand
        {
            get { return this._previousPageCommand; }
        }

        public ICommand NextPageCommand
        {
            get { return this._nextPageCommand; }
        }

        public object CurrentPage
        {
            get { return this.GetValue(CurrentPageProperty); }
            set { this.SetValue(CurrentPageProperty, value); }
        }

        public bool HasNextPage
        {
            get { return (bool) this.GetValue(HasNextPageProperty); }
            set { this.SetValue(HasNextPageProperty, value); }
        }

        public bool HasPreviousPage
        {
            get { return (bool) this.GetValue(HasPreviousPageProperty); }
            set { this.SetValue(HasPreviousPageProperty, value); }
        }

        private static void HandleCurrentPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is IDisplayPage oldPage)
            {
                oldPage.IsCurrent = false;
            }

            if (e.NewValue is IDisplayPage newPage)
            {
                newPage.IsCurrent = true;
            }

            ((PagedContentControl) d)._previousPageCommand.RaiseCanExecuteChanged();
            ((PagedContentControl) d)._nextPageCommand.RaiseCanExecuteChanged();
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            var itemsFromSource = (newValue != null
                ? newValue.OfType<IDisplayPage>()
                : Enumerable.Empty<IDisplayPage>()).ToList();

            this.DistributePageNumbers(itemsFromSource);

            if (oldValue is INotifyCollectionChanged oldcol)
            {
                oldcol.CollectionChanged -= this.HandleItemsInItemssourceChanged;
            }

            if (newValue is INotifyCollectionChanged newcol)
            {
                newcol.CollectionChanged += this.HandleItemsInItemssourceChanged;
            }

            this.SwitchToNextPage();
            base.OnItemsSourceChanged(oldValue, newValue);
        }

        private void HandleItemsInItemssourceChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var itemsFromSource = (this.ItemsSource != null
                ? this.ItemsSource.OfType<IDisplayPage>()
                : Enumerable.Empty<IDisplayPage>()).ToList();

            this.DistributePageNumbers( itemsFromSource);
        }

        private void DistributePageNumbers(IList<IDisplayPage> displayPages)
        {
            var pageNumber = 1;
            foreach (var displayPage in displayPages)
            {
                displayPage.PageNumber = pageNumber++;
            }
        }

        private bool CanExecuteSelectPreviousPage()
        {
            return this.CurrentPage != null && this.HasPreviousPage;
        }

        private bool CanExecuteSelectNextPage()
        {
            return this.CurrentPage != null && this.HasNextPage;
        }

        private void ExecuteSelectPreviousPage()
        {
            if (this.CurrentPage != null)
            {
                this.SwitchToPreviousPage();
            }
        }

        private void SwitchToPreviousPage()
        {
            this.CurrentPage = this.GetPreviousPage();
            this.HasNextPage = this.GetNextPage() != null;
            this.HasPreviousPage = this.GetPreviousPage() != null;
        }

        private void ExecuteSelectNextPage()
        {
            if (this.CurrentPage != null)
            {
                this.SwitchToNextPage();
            }
        }

        private void SwitchToNextPage()
        {
            this.CurrentPage = this.GetNextPage();
            this.HasNextPage = this.GetNextPage() != null;
            this.HasPreviousPage = this.GetPreviousPage() != null;
        }

        private object GetNextPage()
        {
            if (this.ItemsSource == null)
            {
                return null;
            }

            var itemsFromSource = this.ItemsSource.OfType<IDisplayPage>().ToList();
            var currentNode = this.GetCurrentNode(itemsFromSource);
            if (currentNode == null)
            {
                return itemsFromSource.FirstOrDefault();
            }

            return currentNode.Next?.Value;
        }

        private LinkedListNode<object> GetCurrentNode(IList<IDisplayPage> itemsFromSource)
        {
            if (this.CurrentPage != null)
            {
                var linkedList = new LinkedList<object>(itemsFromSource);
                var currentNode = linkedList.Find(this.CurrentPage);
                return currentNode;
            }

            return null;
        }

        private object GetPreviousPage()
        {
            if (this.ItemsSource == null)
            {
                return null;
            }

            var itemsFromSource = this.ItemsSource.OfType<IDisplayPage>().ToList();
            var currentNode = this.GetCurrentNode(itemsFromSource);
            if (currentNode == null)
            {
                return itemsFromSource.FirstOrDefault();
            }

            return currentNode.Previous?.Value;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new PagedContentControl();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is PagedContentControl;
        }
    }
}