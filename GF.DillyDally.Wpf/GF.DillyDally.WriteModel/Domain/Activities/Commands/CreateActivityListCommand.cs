using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Activities.Commands
{
    internal sealed class CreateActivityListCommand : IRequest<CreateActivityListResponse>
    {
    }
}