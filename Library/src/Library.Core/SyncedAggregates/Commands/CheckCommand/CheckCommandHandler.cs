using Library.Core.SyncedAggregate;
using Library.SharedKernel.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Core.SyncedAggregates.Commands.CheckCommand
{

    public class CheckCommandHandler :
            IRequestHandler<CheckCommand>
    {
        private readonly IRepository<Patron> _patronRepository;

        public CheckCommandHandler(
            IRepository<Patron> patronRepository
            )
        {
            _patronRepository = patronRepository;
        }

        public Task<Unit> Handle(CheckCommand request, CancellationToken cancellationToken)
        {
            return Unit.Task;
        }
    }
}
