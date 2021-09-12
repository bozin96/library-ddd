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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate.Commands.ReturnBooks
{
    class ReturnBooksCommandHandler :
         IRequestHandler<ReturnBooksCommand, BaseCommandResponse>
    {
        private readonly IRepository<Library> _libraryRepository;
        private readonly IRepository<Patron> _patronRepository;

        public ReturnBooksCommandHandler(
            IRepository<Library> libraryRepository,
            IRepository<Patron> patronRepository
            )
        {
            _libraryRepository = libraryRepository;
            _patronRepository = patronRepository;
        }

        public async Task<BaseCommandResponse> Handle(ReturnBooksCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {

            }

            BaseCommandResponse response = new BaseCommandResponse();

            // Get Patron.
            Patron patron = await _patronRepository.GetByIdAsync(request.PatronId);
            if (patron == null)
            {
                response.AddError("Wrong patron id", StatusCodes.Status404NotFound);
                return response;
            }

            // Get Library with book requested along with BookLendingRecord and BookReservation record for that user by Id.
            LibraryWithPatronLendedBooksSpec libraryWithPatronLendedBooksSpec = new(request.LibraryId, request.PatronId, request.BookReturningId);
            Library library = await _libraryRepository.GetBySpecAsync(libraryWithPatronLendedBooksSpec, cancellationToken);
            if (library == null)
            {
                response.AddError("Wrong library id", StatusCodes.Status404NotFound);
                return response;
            }

            // Close all lendings.
            library.BookLendings.ForEach(bl => bl.CloseLending());

            // Increase number of copies for all books.
            library.BookLendings.Select(bl => bl.Book).ForEach(b => b.ReturnBookCopy());

            // Save changes to db.
            await _libraryRepository.UpdateAsync(library);

            return response;
        }
    }
}
