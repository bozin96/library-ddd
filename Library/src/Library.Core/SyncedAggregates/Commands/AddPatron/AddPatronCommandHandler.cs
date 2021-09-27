using Library.Core.SyncedAggregate;
using Library.Core.SyncedAggregates.Specifications;
using Library.SharedKernel;
using Library.SharedKernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Core.SyncedAggregates.Commands.AddPatron
{
    public class AddPatronCommandHandler :
        IRequestHandler<AddPatronCommand, BaseCommandResponse>
    {
        private readonly IRepository<Patron> _patronRepository;

        public AddPatronCommandHandler(
            IRepository<Patron> patronRepository
            )
        {
            _patronRepository = patronRepository;
        }

        public async Task<BaseCommandResponse> Handle(AddPatronCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new BaseCommandResponse();
            if (!request.IsValid())
            {
                response.AddErrors(request.ValidationResult.Errors.Select(e => e.ErrorMessage).ToList());
                return response;
            }

            Patron author = new Patron(
                Guid.NewGuid(),
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Email,
                request.Type
                );

            PatronByEmailSpec patronByEmail = new PatronByEmailSpec(request.Email);
            if (await _patronRepository.GetBySpecAsync(patronByEmail, cancellationToken) != null)
            {
                response.AddError("Patron with given email already exist", StatusCodes.Status409Conflict);
                return response;
            }

            await _patronRepository.AddAsync(author);

            return response;
        }
    }
}
