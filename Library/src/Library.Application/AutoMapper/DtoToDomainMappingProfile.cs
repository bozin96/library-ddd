using AutoMapper;
using Library.Application.Dtos.Authors;
using Library.Application.Dtos.BookLendings;
using Library.Application.Dtos.Books;
using Library.Application.Dtos.Patrons;
using Library.Core.LibraryAggregate.Commands.AddBooks;
using Library.Core.LibraryAggregate.Commands.LendBooks;
using Library.Core.LibraryAggregate.Commands.ReserveBooks;
using Library.Core.LibraryAggregate.Commands.ReturnBooks;
using Library.Core.SyncedAggregates.Commands.AddAuthor;
using Library.Core.SyncedAggregates.Commands.AddPatron;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.AutoMapper
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<BookLendingRequest, LendBooksCommand>()
                .ConstructUsing(s => new LendBooksCommand(s.LibraryId, s.BooksIds, s.PatronId));

            CreateMap<BookReturningRequest, ReturnBooksCommand>()
                .ConstructUsing(s => new ReturnBooksCommand(s.LibraryId, s.BookLendingId, s.PatronId));

            CreateMap<BookReservingRequest, ReserveBooksCommand>()
                .ConstructUsing(s => new ReserveBooksCommand(s.LibraryId, s.BooksIds, s.PatronId));

            CreateMap<BookAddRequest, AddBooksCommand>()
                .ConstructUsing(s => new AddBooksCommand(s.LibraryId, s.ISBN, s.Title, s.Description, s.NumberOfPages, s.AuthorId, s.Genre, s.Type, s.NumberOfCopies));

            CreateMap<AuthorAddRequest, AddAuthorCommand>()
                .ConstructUsing(s => new AddAuthorCommand(s.FirstName, s.MiddleName, s.LastName, s.DateOfBirth, s.DateOfDeath));

            CreateMap<PatronAddRequest, AddPatronCommand>()
                .ConstructUsing(s => new AddPatronCommand(s.FirstName, s.MiddleName, s.LastName, s.Email, s.Type));
        }
    }
}
