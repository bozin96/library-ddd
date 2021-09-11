using Library.Application.Dtos;
using Library.Application.Dtos.BookLendings;
using Library.Application.Dtos.Books;
using Library.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface ILibraryService
    {
        BookLendingResponse LendBooks(BookLendingRequest bookLendingRequest);

        BookReturningResponse ReturnBooks(BookReturningRequest bookReturningRequest);

        Task<List<BookResponse>> GetLibraryBooks(Guid libraryId, ResourceParameters resourceParameters);
    }
}
