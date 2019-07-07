using System;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Wpf.Client.Core.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Core.Mvvmc
{
    internal interface IControllerServices : IDisposable
    {
        ReactiveCommandFactory ReactiveCommandFactory { get; }

        NavigationService NavigationService { get; }

        ControllerFactory ControllerFactory { get; }

        IReadModelStore ReadModelStore { get; }

        IMediator Mediator { get; }

        TService GetDomainService<TService>() where TService : IDomainService;
    }
}