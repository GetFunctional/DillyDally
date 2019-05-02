using GF.DillyDally.Mvvmc;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation.Balance
{
    public sealed class BalanceOverviewController : ControllerBase<BalanceOverviewViewModel>
    {
        private readonly AccountBalanceControllerFactory _accountBalanceControllerFactory =
            new AccountBalanceControllerFactory();

        private readonly IMediator _mediator;
        //private List<AccountBalanceController> _accountControllers;

        public BalanceOverviewController(BalanceOverviewViewModel viewModel, IMediator mediator) : base(viewModel)
        {
            this._mediator = mediator;
        }

        //protected override async Task OnInitializeAsync()
        //{
        //    //var accounts = await this._mediator.Send(new GetAllAccountsRequest());

        //    //this._accountControllers = accounts.Select(account =>
        //    //    this._accountBalanceControllerFactory.CreateAccountBalanceController(account)).ToList();

        //    //this.ViewModel.AccountBalances =
        //    //    new ObservableCollection<AccountBalanceViewModel>(this._accountControllers
        //    //        .Select(ctrl => ctrl.ViewModel));
        //}
    }
}