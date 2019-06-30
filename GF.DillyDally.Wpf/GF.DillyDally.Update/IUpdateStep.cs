using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;

namespace GF.DillyDally.Update
{
    public interface IUpdateStep
    {
        Version Version { get; }

        IList<string> GetSqlUpdateScripts();

        Task PerformMigrationsAsync(IMediator mediator);
    }
}