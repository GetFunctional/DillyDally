using System;
using GF.DillyDally.ReadModel.Account;
using GF.DillyDally.ReadModel.Common;
using GF.DillyDally.ReadModel.Tasks;

namespace GF.DillyDally.ReadModel
{
    public sealed class ReadModelInitializer
    {
        public void Initialize(Action<Type, Type> registerType, Action<Type, Type> registerTypeInstance)
        {
            RegisterTypes(registerType, registerTypeInstance);
        }

        private static void RegisterTypes(Action<Type, Type> registerType, Action<Type, Type> registerTypeInstance)
        {
            registerType(typeof(ITasksRepository), typeof(TasksRepository));
            registerType(typeof(ICommonDataRepository), typeof(CommonDataRepository));
            registerType(typeof(IAccountRepository), typeof(AccountRepository));
        }
    }
}