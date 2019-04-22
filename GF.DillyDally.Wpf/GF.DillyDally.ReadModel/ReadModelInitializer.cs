using System;
using GF.DillyDally.ReadModel.Account;
using GF.DillyDally.ReadModel.Common;
using GF.DillyDally.ReadModel.Projection.Categories;
using GF.DillyDally.ReadModel.Projection.Lanes;
using GF.DillyDally.ReadModel.Projection.Rewards;
using GF.DillyDally.ReadModel.Tasks;
using GF.DillyDally.WriteModel.Domain.Categories;
using GF.DillyDally.WriteModel.Domain.Lanes;
using GF.DillyDally.WriteModel.Domain.Rewards;
using GF.DillyDally.WriteModel.Infrastructure;

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

        public void RegisterForDomainEvents(IEventDispatcher eventDispatcher)
        {
            var laneEventHandler = new LaneEventHandler();
            eventDispatcher.RegisterHandler(laneEventHandler);

            var rewardEventHandler = new RewardEventHandler();
            eventDispatcher.RegisterHandler(rewardEventHandler);

            var categoryEventHandler = new CategoryEventHandler();
            eventDispatcher.RegisterHandler(categoryEventHandler);
        }
    }
}