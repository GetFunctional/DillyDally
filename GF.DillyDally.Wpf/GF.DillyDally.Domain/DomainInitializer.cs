using System;
using GF.DillyDally.Contracts.Models.Tasks;
using GF.DillyDally.Domain.Models;

namespace GF.DillyDally.Domain
{
    public sealed class DomainInitializer
    {
        public void InitializeDomainLayer(Action<Type, Type> serviceRegister)
        {
            this.RegisterServices(serviceRegister);
        }

        private void RegisterServices(Action<Type, Type> serviceRegister)
        {
            serviceRegister(typeof(ITaskService), typeof(TaskService));
        }
    }
}