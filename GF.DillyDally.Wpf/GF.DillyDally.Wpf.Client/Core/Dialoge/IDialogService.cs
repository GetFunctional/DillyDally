using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Core.Dialoge
{
    public interface IDialogService
    {
        Task<IDialogResult> ShowDialogAsync(IController dialogContentController, IDialogSettings settings);
    }
}