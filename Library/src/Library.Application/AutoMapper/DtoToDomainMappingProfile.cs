using AutoMapper;
using Library.Application.Dtos.BookLendings;
using Library.Core.LibraryAggregate.Commands.BookLending;
using Library.Core.LibraryAggregate.Commands.BookReturning;
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
            CreateMap<BookLendingRequest, BookLendingCommand>()
                .ConstructUsing(s => new BookLendingCommand(s.LibraryId, s.BooksIds, s.PatronId));

            CreateMap<BookReturningRequest, BookReturningCommand>()
                .ConstructUsing(s => new BookReturningCommand(s.LibraryId, s.BookLendingId, s.PatronId));
        }
    }
}
