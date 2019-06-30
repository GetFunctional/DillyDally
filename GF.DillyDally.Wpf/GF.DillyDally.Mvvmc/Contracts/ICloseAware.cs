namespace GF.DillyDally.Mvvmc.Contracts
{
    public interface ICloseAware 
    {
        bool ConfirmClosing(object callSource);

        void Close();
    }
}