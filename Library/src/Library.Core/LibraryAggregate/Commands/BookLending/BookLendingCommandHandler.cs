using LaYumba.Functional;
using Library.Core.LibraryAggregate.Commands;
using Library.Core.LibraryAggregate.Specifications;
using Library.Core.LibraryAggregate;
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
using Library.Core.SyncedAggregate;

namespace Library.Core.LibraryAggregate.Commands.BookLending
{
    public class BookLendingCommandHandler :
        IRequestHandler<BookLendingCommand, BaseCommandResponse>
    {
        private readonly IRepository<Library> _libraryRepository;
        private readonly IRepository<Patron> _patronRepository;

        public BookLendingCommandHandler(
            IRepository<Library> libraryRepository,
            IRepository<Patron> patronRepository
            )
        {
            _libraryRepository = libraryRepository;
            _patronRepository = patronRepository;
        }

        async Task<BaseCommandResponse> IRequestHandler<BookLendingCommand, BaseCommandResponse>.Handle(BookLendingCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {

            }

            BaseCommandResponse response = new BaseCommandResponse();

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
            LibraryWithPatronLendedAndReservedBooksSpec libraryWithPatronLendedBooksSpec = new(request.LibraryId, request.PatronId, request.BooksIds);
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


            var responseData = new List<LibraryAggregate.BookLending>();
            // Create BookLending records.
            foreach (Book book in library.Books)
            {
                // neki transactionId da poveze one koje su iznajmljene zajedno.
                LibraryAggregate.BookLending bookLending = new LibraryAggregate.BookLending(Guid.NewGuid(), request.PatronId, book, DateTime.Now.AddDays(30));
                responseData.Add(bookLending);
                library.AddNewBookLending(bookLending);
            }

            // Save changes to db.
            await _libraryRepository.UpdateAsync(library);

            response.Data = responseData;
            return response;
        }
    }
}
