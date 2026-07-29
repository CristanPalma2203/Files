using Application.Common;
using Application.Dtos;
using Domain.Service;
using MediatR;

namespace Application.Services.Comandos
{
    public class CommandBus : ICommandBus
    {
        private readonly IMediator mediator;

        public CommandBus(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IResponse execute(IMessage comando)
        {
            return mediator.Send((IAppMessage)comando).GetAwaiter().GetResult();
        }
    }
}
