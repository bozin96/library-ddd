using AutoMapper;
using Library.Application.Interfaces;
using Library.Application.Dtos.BookLendings;
using Library.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Core.LibraryAggregate.Commands.BookLending;
using Library.SharedKernel;
using Library.Core.LibraryAggregate;
using Library.Application.Dtos.Books;
using Library.Core.LibraryAggregate.Specifications;
using Library.Core.LibraryAggregate.Commands.BookReturning;

namespace Library.Application.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        private readonly IRepository<Core.LibraryAggregate.Library> _libraryRepository;

        public LibraryService(
            IMapper mapper,
            IMediatorHandler mediator,
            IRepository<Core.LibraryAggregate.Library> libraryRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _libraryRepository = libraryRepository;
        }

        public BookLendingResponse LendBooks(BookLendingRequest bookLendingRequest)
        {
            var bookLendingCommand = _mapper.Map<BookLendingCommand>(bookLendingRequest);
            var baseCommandResponse = _mediator.SendCommand<BookLendingCommand, BaseCommandResponse>(bookLendingCommand).Result;
            var response = _mapper.Map<BookLendingResponse>(baseCommandResponse);

            return response;
        }

        public BookReturningResponse ReturnBooks(BookReturningRequest bookReturningRequest)
        {
            var bookReturningCommand = _mapper.Map<BookReturningCommand>(bookReturningRequest);
            var baseCommandResponse = _mediator.SendCommand<BookReturningCommand, BaseCommandResponse>(bookReturningCommand).Result;
            var response = _mapper.Map<BookReturningResponse>(baseCommandResponse);

            return response;
        }

        public async Task<List<BookResponse>> GetLibraryBooks(Guid libraryId, ResourceParameters resourceParameters)
        {
            LibraryWithPaginatedBooks libraryWithPaginatedBooks = new LibraryWithPaginatedBooks(libraryId, resourceParameters);
            var libraryBooks = await _libraryRepository.GetBySpecAsync(libraryWithPaginatedBooks);
            var books = _mapper.Map<List<BookResponse>>(libraryBooks.Books);

            return books;
        }
    }
}
