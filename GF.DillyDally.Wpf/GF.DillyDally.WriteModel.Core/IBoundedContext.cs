using LightInject;

namespace GF.DillyDally.WriteModel.Core
{
    public interface IBoundedContext
    {
        void Initialize(IServiceContainer serviceContainer);
    }
}