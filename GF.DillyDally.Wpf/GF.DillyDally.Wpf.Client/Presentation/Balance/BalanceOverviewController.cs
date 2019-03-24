using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Contracts.Keys;
using GF.DillyDally.Data.Account;
using GF.DillyDally.Data.Common;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Common;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation.Balance
{
    public sealed class BalanceOverviewController : ControllerBase<BalanceOverviewViewModel>
    {
        private readonly IMediator _mediator;
        private readonly AccountBalanceControllerFactory _accountBalanceControllerFactory = new AccountBalanceControllerFactory();
        private List<AccountBalanceController> _currencyControllers;

        public BalanceOverviewController(BalanceOverviewViewModel viewModel, IMediator mediator) : base(viewModel)
        {
            this._mediator = mediator;
        }

        protected override async Task OnInitializeAsync()
        {
            var currencies = await this._mediator.Send(new GetAllCurrenciesRequest());

            this._currencyControllers = currencies.Select(currency =>
                this._accountBalanceControllerFactory.CreateAccountBalanceController(new AccountEntity()
                {
                    Currency = currency,
                    Balance = 0.0m,
                    AccountKey = new AccountKey(Guid.NewGuid())
                })).ToList();

            this.ViewModel.AccountBalances =
                new ObservableCollection<AccountBalanceViewModel>(this._currencyControllers
                    .Select(ctrl => ctrl.ViewModel));
        }
    }
}