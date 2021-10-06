using AutoMapper;
using Library.Application.Dtos.Authors;
using Library.Application.Interfaces;
using Library.Core.LibraryAggregate.Commands.CheckBookLendings;
using Library.Core.SyncedAggregate;
using Library.Core.SyncedAggregates.Commands.AddAuthor;
using Library.Core.SyncedAggregates.Commands.CheckCommand;
using Library.Core.SyncedAggregates.Specifications;
using Library.SharedKernel;
using Library.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        private readonly IRepository<Author> _authorRepository;


        public AuthorService(
            IMapper mapper,
            IMediatorHandler mediator,
            IRepository<Author> authorRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _authorRepository = authorRepository;
        }

        public async Task<List<AuthorResponse>> GetAuthors(ResourceParameters resourceParameters)
        {
            PaginatedAuthorsSpec paginatedAuthorsSpec = new PaginatedAuthorsSpec(resourceParameters);
            var paginatedAuthors = await _authorRepository.ListAsync(paginatedAuthorsSpec);
            var authors = _mapper.Map<List<AuthorResponse>>(paginatedAuthors);

            return authors;
        }

        public AuthorAddResponse AddAuthor(AuthorAddRequest request)
        {
            var authorAddCommand = _mapper.Map<AddAuthorCommand>(request);
            var baseCommandResponse = _mediator.SendCommand(authorAddCommand).Result;
            var response = _mapper.Map<AuthorAddResponse>(baseCommandResponse);

            _mediator.ScheduleCommand(new CheckCommand()
            {
                Proba = request.FirstName,
                Proba2 = request.LastName,
                Test = 1
            }, TimeSpan.FromMinutes(1));

            //_mediator.ScheduleCommand(new CheckBookLendingsCommand(Guid.NewGuid(), new List<Guid>() { Guid.NewGuid() }, Guid.NewGuid(), request.FirstName));

            return response;
        }
    }
}
