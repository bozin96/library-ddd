using AutoMapper;
using Library.Application.Interfaces;
using Library.Application.Dtos.BookLendings;
using Library.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Core.LibraryAggregate.Commands.LendBooks;
using Library.SharedKernel;
using Library.Core.LibraryAggregate;
using Library.Application.Dtos.Books;
using Library.Core.LibraryAggregate.Specifications;
using Library.Core.LibraryAggregate.Commands.ReturnBooks;
using Library.Core.LibraryAggregate.Commands.ReserveBooks;

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
            var bookLendingCommand = _mapper.Map<LendBooksCommand>(bookLendingRequest);
            var baseCommandResponse = _mediator.SendCommand<LendBooksCommand, BaseCommandResponse>(bookLendingCommand).Result;
            var response = _mapper.Map<BookLendingResponse>(baseCommandResponse);

            return response;
        }

        public BookReturningResponse ReturnBooks(BookReturningRequest bookReturningRequest)
        {
            var bookReturningCommand = _mapper.Map<ReturnBooksCommand>(bookReturningRequest);
            var baseCommandResponse = _mediator.SendCommand<ReturnBooksCommand, BaseCommandResponse>(bookReturningCommand).Result;
            var response = _mapper.Map<BookReturningResponse>(baseCommandResponse);

            return response;
        }

        public BookReservingResponse ReserveBooks(BookReservingRequest bookReservingRequest)
        {
            var bookReservingCommand = _mapper.Map<ReserveBooksCommand>(bookReservingRequest);
            var baseCommandResponse = _mediator.SendCommand<ReserveBooksCommand, BaseCommandResponse>(bookReservingCommand).Result;
            var response = _mapper.Map<BookReservingResponse>(baseCommandResponse);

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
