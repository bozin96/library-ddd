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
using Library.Core.LibraryAggregate.Commands.AddBooks;
using Library.Core.LibraryAggregate.Commands.CheckBookLendings;
using Newtonsoft.Json;

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
            var baseCommandResponse = _mediator.SendCommand(bookLendingCommand).Result;
            if (baseCommandResponse.Data is List<BookLending>)
            {
                List<BookLending> bookLendings = (List<BookLending>)baseCommandResponse.Data;
                var checkCommandContent = new CheckBookLendingsCommandContent(bookLendingRequest.LibraryId, bookLendingRequest.PatronId, bookLendings.Select(bl => bl.Id).ToList());
                var checkCommand = new CheckBookLendingsCommand(JsonConvert.SerializeObject(checkCommandContent));
                _mediator.ScheduleCommand(checkCommand, TimeSpan.FromMinutes(1));
            }
            var response = _mapper.Map<BookLendingResponse>(baseCommandResponse);

            return response;
        }

        public BookReturningResponse ReturnBooks(BookReturningRequest bookReturningRequest)
        {
            var bookReturningCommand = _mapper.Map<ReturnBooksCommand>(bookReturningRequest);
            var baseCommandResponse = _mediator.SendCommand(bookReturningCommand).Result;
            var response = _mapper.Map<BookReturningResponse>(baseCommandResponse);

            return response;
        }

        public BookReservingResponse ReserveBooks(BookReservingRequest bookReservingRequest)
        {
            var bookReservingCommand = _mapper.Map<ReserveBooksCommand>(bookReservingRequest);
            var baseCommandResponse = _mediator.SendCommand(bookReservingCommand).Result;
            var response = _mapper.Map<BookReservingResponse>(baseCommandResponse);

            return response;
        }

        public async Task<List<BookResponse>> GetLibraryBooks(Guid libraryId, ResourceParameters resourceParameters)
        {
            LibraryWithPaginatedBooksSpec libraryWithPaginatedBooks = new LibraryWithPaginatedBooksSpec(libraryId, resourceParameters);
            var libraryBooks = await _libraryRepository.GetBySpecAsync(libraryWithPaginatedBooks);
            var books = _mapper.Map<List<BookResponse>>(libraryBooks.Books);

            return books;
        }

        public BookAddResponse AddBook(Guid libraryId, BookAddRequest bookAddRequest)
        {
            var bookAddCommand = _mapper.Map<AddBooksCommand>(bookAddRequest);
            var baseCommandResponse = _mediator.SendCommand(bookAddCommand).Result;
            var response = _mapper.Map<BookAddResponse>(baseCommandResponse);

            return response;
        }
    }
}
