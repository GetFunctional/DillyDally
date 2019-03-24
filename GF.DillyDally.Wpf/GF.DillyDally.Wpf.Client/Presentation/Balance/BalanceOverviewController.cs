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
        private List<AccountBalanceController> _accountControllers;

        public BalanceOverviewController(BalanceOverviewViewModel viewModel, IMediator mediator) : base(viewModel)
        {
            this._mediator = mediator;
        }

        protected override async Task OnInitializeAsync()
        {
            var accounts = await this._mediator.Send(new GetAllAccountsRequest());

            this._accountControllers = accounts.Select(account =>
                this._accountBalanceControllerFactory.CreateAccountBalanceController(account)).ToList();

            this.ViewModel.AccountBalances =
                new ObservableCollection<AccountBalanceViewModel>(this._accountControllers
                    .Select(ctrl => ctrl.ViewModel));
        }
    }
}