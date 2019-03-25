using System;
using GF.DillyDally.Data.Account;
using GF.DillyDally.Data.Common;
using GF.DillyDally.Data.Tasks;

namespace GF.DillyDally.Data
{
    public sealed class DataInitializer
    {
        public void InitializeDataLayer(Action<Type, Type> serviceRegister)
        {
            RegisterTypes(serviceRegister);
        }

        private static void RegisterTypes(Action<Type, Type> serviceRegister)
        {
            serviceRegister(typeof(ITasksRepository), typeof(TasksRepository));
            serviceRegister(typeof(ICommonDataRepository), typeof(CommonDataRepository));
            serviceRegister(typeof(IAccountRepository), typeof(AccountRepository));
        }
    }
}