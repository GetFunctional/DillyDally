using System;

namespace GF.DillyDally.WriteModel
{
    public sealed class WriteModelInitializer
    {
        public void Initialize(Action<Type, Type> serviceRegister)
        {
            this.RegisterServices(serviceRegister);
        }

        private void RegisterServices(Action<Type, Type> serviceRegister)
        {
            serviceRegister(typeof(ITaskService), typeof(TaskService));
        }
    }
}