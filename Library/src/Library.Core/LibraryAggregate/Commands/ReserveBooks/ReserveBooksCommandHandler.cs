using LaYumba.Functional;
using Library.Core.LibraryAggregate.Specifications;
using Library.Core.SyncedAggregate;
using Library.SharedKernel;
using Library.SharedKernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate.Commands.ReserveBooks
{
    public class ReserveBooksCommandHandler :
        IRequestHandler<ReserveBooksCommand, BaseCommandResponse>
    {
        private readonly IRepository<Library> _libraryRepository;
        private readonly IRepository<Patron> _patronRepository;

        public ReserveBooksCommandHandler(
            IRepository<Library> libraryRepository,
            IRepository<Patron> patronRepository
            )
        {
            _libraryRepository = libraryRepository;
            _patronRepository = patronRepository;
        }

        public async Task<BaseCommandResponse> Handle(ReserveBooksCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new BaseCommandResponse();
            if (!request.IsValid())
            {
                response.AddErrors(request.ValidationResult.Errors.Select(e => e.ErrorMessage).ToList());
                return response;
            }

            // Get Patron.
            Patron patron = await _patronRepository.GetByIdAsync(request.PatronId);
            if (patron == null)
            {
                response.AddError("Wrong patron id", StatusCodes.Status404NotFound);
                return response;
            }

            // Get Library with requested books along with active BookLendings and BookReservations record for that user by Id.
            LibraryWithPatronLentAndReservedBooksSpec libraryWithPatronLendedBooksSpec = new(request.LibraryId, request.PatronId, request.BooksIds);
            Library library = await _libraryRepository.GetBySpecAsync(libraryWithPatronLendedBooksSpec, cancellationToken);
            if (library == null)
            {
                response.AddError("Wrong library id", StatusCodes.Status404NotFound);
                return response;
            }

            // Check if patron can reserve a books.
            DomainActionStatus canPatronReserveBooksStatus = library.CanPatronReserveBooks(patron, request.BooksIds);
            if (!canPatronReserveBooksStatus.Status)
            {
                response.AddError(canPatronReserveBooksStatus.Error, canPatronReserveBooksStatus.StatusCode);
                return response;
            }

            //reduce number of available copies
            library.Books.ForEach(b => b.LendBookCopy());

            var responseData = new List<BookReservation>();
            // Create BookReservation records.
            foreach (Book book in library.Books)
            {
                // neki transactionId da poveze one koje su iznajmljene zajedno.
                BookReservation bookReservation = new BookReservation(Guid.NewGuid(), request.PatronId, book.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(30));
                responseData.Add(bookReservation);
                library.AddNewBookReservation(bookReservation);
            }

            // Save changes to db.
            await _libraryRepository.UpdateAsync(library);

            response.Data = responseData;
            return response;
        }
    }
}
