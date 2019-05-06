using ReactiveUI;

namespace GF.DillyDally.Mvvmc
{
    /// <summary>
    ///     Klasse für die Implementation von INotifyPropertyChanged und INotifyPropertyChanging
    ///     Die beiden Event-Methoden werden deklariert, sowie eine Hilfsmethode RaiseAndSetIfChanged um den
    ///     Wert einer Eigenschaft zu setzen.
    /// </summary>
    public class ObservableObject : ReactiveObject
    {
    }
}