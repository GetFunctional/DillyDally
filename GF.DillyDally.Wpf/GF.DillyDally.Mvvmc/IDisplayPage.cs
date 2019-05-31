using GF.DillyDally.Mvvmc.Validation;

namespace GF.DillyDally.Mvvmc
{
    public interface IDisplayPage : IViewModel, IValidateable
    {
        string Title { get; }

        bool IsCurrent { get; set; }

        int PageNumber { get; set; }
    }
}