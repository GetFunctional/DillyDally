using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Wpf.Client.Core.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Core.Mvvmc
{
    internal sealed class ControllerServices : IControllerServices
    {
        private readonly IServiceContainer _serviceContainer;

        public ControllerServices(ControllerFactory controllerFactory, IReadModelStore readModelStore,
            IServiceContainer serviceContainer, IMediator mediator, NavigationService navigationService,
            CommandFactory commandFactory)
        {
            this._serviceContainer = serviceContainer;
            this.Mediator = mediator;
            this.NavigationService = navigationService;
            this.CommandFactory = commandFactory;
            this.ControllerFactory = controllerFactory;
            this.ReadModelStore = readModelStore;
        }

        public IMediator Mediator { get; }

        #region IControllerServices Members

        public CommandFactory CommandFactory { get; }
        public NavigationService NavigationService { get; }
        public ControllerFactory ControllerFactory { get; }
        public IReadModelStore ReadModelStore { get; }

        public TService GetDomainService<TService>() where TService : IDomainService
        {
            return this._serviceContainer.GetInstance<TService>();
        }

        #endregion
    }
}