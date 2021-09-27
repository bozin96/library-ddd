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

namespace Library.Core.SyncedAggregates.Commands.AddAuthor
{
    public class AddAuthorCommandHandler :
        IRequestHandler<AddAuthorCommand, BaseCommandResponse>
    {
        private readonly IRepository<Author> _authorRepository;

        public AddAuthorCommandHandler(
            IRepository<Author> authorRepository
            )
        {
            _authorRepository = authorRepository;
        }

        public async Task<BaseCommandResponse> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new BaseCommandResponse();
            if (!request.IsValid())
            {
                response.AddErrors(request.ValidationResult.Errors.Select(e => e.ErrorMessage).ToList());
                return response;
            }

            Author author = new Author(
                Guid.NewGuid(),
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.DateOfBirth,
                request.DateOfDeath
                );

            AuthorByFullNameSpec authorByFullName = new AuthorByFullNameSpec(request.FirstName, request.MiddleName, request.LastName);
            if (await _authorRepository.GetBySpecAsync(authorByFullName, cancellationToken) != null)
            {
                response.AddError("Author with given name already exist", StatusCodes.Status409Conflict);
                return response;
            }

            await _authorRepository.AddAsync(author);

            return response;
        }
    }
}
