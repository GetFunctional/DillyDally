﻿using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation.Rewards
{
    /// <summary>
    ///     Interaktionslogik für AccountsView.xaml
    /// </summary>
    public partial class AccountsView : IViewFor<AccountsViewModel>
    {
        #region Constructors

        #region - Konstruktoren -

        public AccountsView()
        {
            this.InitializeComponent();
        }

        #endregion

        #endregion

        #region Properties, Indexers

        #region - Properties oeffentlich -

        public AccountsViewModel ViewModel
        {
            get { return (AccountsViewModel) this.DataContext; }
        }

        #endregion

        #endregion
    }
}