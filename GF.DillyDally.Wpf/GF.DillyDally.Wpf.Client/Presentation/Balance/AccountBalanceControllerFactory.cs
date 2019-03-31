using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.ReadModel.Account;
using GF.DillyDally.Wpf.Client.Presentation.Common;

namespace GF.DillyDally.Wpf.Client.Presentation.Balance
{
    public sealed class AccountBalanceControllerFactory
    {
        private readonly CommonDataViewModelFactory _commonDataViewModelFactory = new CommonDataViewModelFactory();
        private readonly ControllerInitializer _controllerInitializer = new ControllerInitializer();

        //public AccountBalanceController CreateAccountBalanceController(AccountBalanceViewModel viewModel, ICurrencyEntity currencyEntity)
        //{
        //    var currencyViewModel = this._commonDataViewModelFactory.CreateCurrentViewModelFrom(currencyEntity);
        //    var viewModel = new AccountBalanceViewModel(currencyViewModel, accountBalanceEntity.);
        //    var controller = new AccountBalanceController(viewModel);
        //    this._controllerInitializer.InitializeController(controller);
        //    return controller;
        //}
    }
}