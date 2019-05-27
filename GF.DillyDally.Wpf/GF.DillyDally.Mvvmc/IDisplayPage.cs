namespace GF.DillyDally.Mvvmc
{
    public interface IDisplayPage : IViewModel
    {
        string Title { get; }

        bool IsCurrent { get; set; }

        int PageNumber { get; set; }
    }
}