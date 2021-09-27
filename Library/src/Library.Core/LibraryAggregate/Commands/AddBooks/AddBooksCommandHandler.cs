using Library.Core.LibraryAggregate.Specifications;
using Library.Core.SyncedAggregate;
using Library.Core.ValueObjects;
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

namespace Library.Core.LibraryAggregate.Commands.AddBooks
{
    public class AddBooksCommandHandler :
        IRequestHandler<AddBooksCommand, BaseCommandResponse>
    {
        private readonly IRepository<Library> _libraryRepository;
        private readonly IRepository<Author> _authorRepository;

        public AddBooksCommandHandler(
            IRepository<Library> libraryRepository,
            IRepository<Author> authorRepository
            )
        {
            _libraryRepository = libraryRepository;
            _authorRepository = authorRepository;
        }

        public async Task<BaseCommandResponse> Handle(AddBooksCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new BaseCommandResponse();
            if (!request.IsValid())
            {
                response.AddErrors(request.ValidationResult.Errors.Select(e => e.ErrorMessage).ToList());
                return response;
            }

            ISBN isbn = new ISBN(request.ISBN);

            Book book = new Book(
                Guid.NewGuid(),
                request.LibraryId,
                isbn,
                request.Title,
                request.Description,
                request.NumberOfPages,
                request.AuthorId,
                request.Genre,
                request.Type,
                request.NumberOfCopies
                );

            LibraryWithExistingBooksByISBNSpec libraryWithExistingBooks = new LibraryWithExistingBooksByISBNSpec(request.LibraryId, request.ISBN);
            Library library = await _libraryRepository.GetBySpecAsync(libraryWithExistingBooks, cancellationToken);

            if(library == null)
            {
                response.AddError("Library with given id does not exist", StatusCodes.Status409Conflict);
                return response;
            }
            if (library.Books.Any())
            {
                response.AddError("Book with given isbn already exist", StatusCodes.Status409Conflict);
                return response;
            }
            if(await _authorRepository.GetByIdAsync(request.AuthorId) == null)
            {
                response.AddError("Author with given id does not exist", StatusCodes.Status409Conflict);
                return response;
            }

            library.AddNewBook(book);
            await _libraryRepository.UpdateAsync(library);

            return response;
        }
    }

}
