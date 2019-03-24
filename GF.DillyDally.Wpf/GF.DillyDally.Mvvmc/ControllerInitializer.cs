using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc.Exceptions;

namespace GF.DillyDally.Mvvmc
{
    public sealed class ControllerInitializer
    {
        public void InitializeController(IController controller)
        {
            InitializeInternal((InitializationBase) controller);
        }

        private static void InitializeInternal(InitializationBase initializeComponent)
        {
            initializeComponent.Initialize();
            var cancellationToken = new CancellationTokenSource();

            var currentSynchronizationContext = SynchronizationContext.Current;
            Task.Run(() => initializeComponent.InitializeAsync(cancellationToken.Token),
                cancellationToken.Token).ContinueWith(t =>
            {
                currentSynchronizationContext.Send(state =>
                    throw new InitializationException("Exception was raised during initialization", t.Exception), null);
            }, TaskContinuationOptions.OnlyOnFaulted).ConfigureAwait(false);
        }
    }
}