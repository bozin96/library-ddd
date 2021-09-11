using Library.SharedKernel.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.SharedKernel
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task RaiseEvent<T>(T @event) where T : BaseDomainEvent
        {
            return _mediator.Publish(@event);
        }

        public Task<U> SendCommand<T, U>(T command) where T : BaseCommand<U> where U : BaseCommandResponse
        {
            return _mediator.Send(command);
        }
    }
}
