using System;
using GF.DillyDally.ReadModel.Account;
using GF.DillyDally.ReadModel.Common;
using GF.DillyDally.ReadModel.Tasks;

namespace GF.DillyDally.ReadModel
{
    public sealed class ReadModelInitializer
    {
        public void Initialize(Action<Type, Type> serviceRegister)
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