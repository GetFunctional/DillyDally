using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Wpf.Client.Core.Commands;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.Wpf.Client.Core.Mvvmc
{
    internal interface IControllerServices
    {
        CommandFactory CommandFactory { get; }

        NavigationService NavigationService { get; }

        ControllerFactory ControllerFactory { get; }

        IDbConnectionFactory DbConnectionFactory { get; }

        TService GetDomainService<TService>() where TService : IDomainService;
    }
}