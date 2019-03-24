using GF.DillyDally.Data.Account;
using GF.DillyDally.Data.Common;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Common;

namespace GF.DillyDally.Wpf.Client.Presentation.Balance
{
    public sealed class AccountBalanceControllerFactory
    {
        private readonly CommonDataViewModelFactory _commonDataViewModelFactory = new CommonDataViewModelFactory();
        private readonly ControllerInitializer _controllerInitializer = new ControllerInitializer();

        public AccountBalanceController CreateAccountBalanceController(AccountEntity accountEntity)
        {
            var currencyViewModel = this._commonDataViewModelFactory.CreateCurrentViewModelFrom(accountEntity.Currency);
            var viewModel = new AccountBalanceViewModel(currencyViewModel, 0.0m);
            var controller = new AccountBalanceController(viewModel);
            this._controllerInitializer.InitializeController(controller);
            return controller;
        }
    }
}