using LaYumba.Functional;
using Library.Core.LibraryAggregate.Specifications;
using Library.SharedKernel;
using Library.SharedKernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Library.Core.SyncedAggregate;
using Library.Core.LibraryAggregate.Events.BookLended;

namespace Library.Core.LibraryAggregate.Commands.LendBooks
{
    public class LendBooksCommandHandler :
        IRequestHandler<LendBooksCommand, BaseCommandResponse>
    {
        private readonly IRepository<Library> _libraryRepository;
        private readonly IRepository<Patron> _patronRepository;

        public LendBooksCommandHandler(
            IRepository<Library> libraryRepository,
            IRepository<Patron> patronRepository
            )
        {
            _libraryRepository = libraryRepository;
            _patronRepository = patronRepository;
        }

        async Task<BaseCommandResponse> IRequestHandler<LendBooksCommand, BaseCommandResponse>.Handle(LendBooksCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new BaseCommandResponse();
            if (!request.IsValid())
            {
                response.AddErrors(request.ValidationResult.Errors.Select(e => e.ErrorMessage).ToList());
                return response;
            }

            // Provera koji je user, da li moze da uzme toliko knjiga.
            // Koje su knjige, da li su tipa da mogu da se iznose i koji je user, koji je broj kopija ostao i to.
            // Dodaj tip user-a da bude klasa a da se samo enum upisuje u bazu, broj maks knjiga da bude samo staticki u memoriji za taj objekat.
            // Proveri da li ima rezervaciju u tom periodu za tu knjigu i skini rezervaciju.
            // Provera da li vec ima uzetu tu knjigu jer ne moze da iznajmi knjigu koju vec ima.
            // ako je bila rezervisana onda treba da ucini rezervaciju neatkivnom.

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

            // Check if patron can lend a books.
            DomainActionStatus canPatronLendBooksStatus = library.CanPatronLendBooks(patron, request.BooksIds);
            if (!canPatronLendBooksStatus.Status)
            {
                response.AddError(canPatronLendBooksStatus.Error, canPatronLendBooksStatus.StatusCode);
                return response;
            }

            // Deactivate reservations if book is in the list.
            foreach (BookReservation bookReservation in library.BookReservations.Where(br => request.BooksIds.Contains(br.BookId)))
            {
                bookReservation.CloseReservation();
            }

            //reduce number of available copies
            library.Books.ForEach(b => b.LendBookCopy());

            List<BookLending> responseData = new List<BookLending>();
            // Create BookLending records.
            foreach (Book book in library.Books)
            {
                // neki transactionId da poveze one koje su iznajmljene zajedno.
                BookLending bookLending = new BookLending(Guid.NewGuid(), request.PatronId, book, DateTime.Now.AddDays(30));

                // Add BookLentEvent.
                //var bookLentEvent = new BookLentEvent(bookLending.BookId, bookLending.PatronId);
                //bookLending.Events.Add(bookLentEvent);

                // Add BookLending to Library.
                library.AddNewBookLending(bookLending);
                responseData.Add(bookLending);
            }

            // Save changes to db.
            await _libraryRepository.UpdateAsync(library);

            response.Data = responseData;
            return response;
        }
    }
}
