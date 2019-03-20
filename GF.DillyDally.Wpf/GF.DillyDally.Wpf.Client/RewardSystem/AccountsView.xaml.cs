﻿using GF.DillyDally.Wpf.Client.Core;

namespace GF.DillyDally.Wpf.Client.RewardSystem
{
    /// <summary>
    /// Interaktionslogik für AccountsView.xaml
    /// </summary>
    public partial class AccountsView : IViewFor<AccountsViewModel>
    {
        #region - Konstruktoren -

        public AccountsView()
        {
            InitializeComponent();
        }

        #endregion

        #region - Properties oeffentlich -

        public AccountsViewModel ViewModel
        {
            get
            {
                return (AccountsViewModel)this.DataContext;
            }
        }

        #endregion
    }
}