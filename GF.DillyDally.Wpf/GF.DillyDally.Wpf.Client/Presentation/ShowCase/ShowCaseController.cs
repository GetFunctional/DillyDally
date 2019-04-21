using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Rewards;

namespace GF.DillyDally.Wpf.Client.Presentation.ShowCase
{
    internal class ShowCaseController: ControllerBase<ShowCaseViewModel>
    {
        public ShowCaseController(ShowCaseViewModel viewModel) : base(viewModel)
        {
        }
    }
}
