using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Core.Dialoge
{
    public interface IDialogService
    {
        IDialogResult ShowDialog(IController dialogContentController, IDialogSettings settings);

        Task<IDialogResult> ShowDialogAsync(IController dialogContentController, IDialogSettings settings);
    }
}