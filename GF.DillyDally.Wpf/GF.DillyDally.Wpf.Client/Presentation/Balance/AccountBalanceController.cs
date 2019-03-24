using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Balance
{
    public sealed class AccountBalanceController : ControllerBase<AccountBalanceViewModel>
    {
        public AccountBalanceController(AccountBalanceViewModel viewModel) : base(viewModel)
        {
        }
    }
}