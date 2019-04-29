using GF.DillyDally.ReadModel.Deprecated.Account;
using GF.DillyDally.ReadModel.Deprecated.Common;
using GF.DillyDally.ReadModel.Projection.Achievements;
using GF.DillyDally.ReadModel.Projection.Categories;
using GF.DillyDally.ReadModel.Projection.Lanes;
using GF.DillyDally.ReadModel.Projection.Rewards;
using GF.DillyDally.ReadModel.Projection.Tasks;
using GF.DillyDally.ReadModel.Repository;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;

namespace GF.DillyDally.ReadModel
{
    public sealed class ReadModelInitializer
    {
        public void Initialize(IServiceContainer serviceContainer, IEventDispatcher eventDispatcher)
        {
            RegisterTypes(serviceContainer);
            this.RegisterForDomainEvents(serviceContainer, eventDispatcher);
        }

        private static void RegisterTypes(IServiceContainer serviceContainer)
        {
            serviceContainer.Register<ICategoryRepository, CategoryRepository>();
            serviceContainer.Register<ILaneRepository, LaneRepository>();
            serviceContainer.Register<ICommonDataRepository, CommonDataRepository>();
            serviceContainer.Register<IAccountRepository, AccountRepository>();
            serviceContainer.Register<ITaskRepository, TaskRepository>();
            serviceContainer.Register<IAchievementRepository, AchievementRepository>();
        }

        private void RegisterForDomainEvents(IServiceContainer serviceContainer, IEventDispatcher eventDispatcher)
        {
            var laneEventHandler = serviceContainer.Create<LaneEventHandler>();
            eventDispatcher.RegisterHandler(laneEventHandler);

            var rewardEventHandler = serviceContainer.Create<RewardEventHandler>();
            eventDispatcher.RegisterHandler(rewardEventHandler);

            var categoryEventHandler = serviceContainer.Create<CategoryEventHandler>();
            eventDispatcher.RegisterHandler(categoryEventHandler);

            var taskEventHandler = serviceContainer.Create<TaskEventHandler>();
            eventDispatcher.RegisterHandler(taskEventHandler);

            var achievementEventHandler = serviceContainer.Create<AchievementEventHandler>();
            eventDispatcher.RegisterHandler(achievementEventHandler);
        }
    }
}