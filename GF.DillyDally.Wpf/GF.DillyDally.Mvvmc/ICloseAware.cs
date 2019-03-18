namespace GF.DillyDally.Mvvmc
{
    public interface ICloseAware
    {
        bool ConfirmClosing(object callSource);

        void Close();
    }
}