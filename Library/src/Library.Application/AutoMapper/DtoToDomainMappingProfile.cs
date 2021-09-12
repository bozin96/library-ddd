using AutoMapper;
using Library.Application.Dtos.BookLendings;
using Library.Core.LibraryAggregate.Commands.LendBooks;
using Library.Core.LibraryAggregate.Commands.ReserveBooks;
using Library.Core.LibraryAggregate.Commands.ReturnBooks;
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
        }
    }
}
