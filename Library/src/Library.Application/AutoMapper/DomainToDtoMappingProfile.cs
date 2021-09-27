using AutoMapper;
using Library.Application.Dtos.Authors;
using Library.Application.Dtos.BookLendings;
using Library.Application.Dtos.Books;
using Library.Application.Dtos.Patrons;
using Library.Core.LibraryAggregate;
using Library.Core.LibraryAggregate.Commands.LendBooks;
using Library.Core.SyncedAggregate;
using Library.SharedKernel;
using Library.SharedKernel.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.AutoMapper
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<BaseCommandResponse, BookLendingResponse>()
                .ConstructUsing(s => new BookLendingResponse(s.Status, s.StatusCode, s.Errors, s.Data));

            CreateMap<BaseCommandResponse, BookReturningResponse>()
                .ConstructUsing(s => new BookReturningResponse(s.Status, s.StatusCode, s.Errors, s.Data));

            CreateMap<Book, BookResponse>()
                .ForMember(
                    dest => dest.ISBN,
                    opt => opt.MapFrom(src => src.ISBN.Value))
                .ForMember(
                    dest => dest.Genre,
                    opt => opt.MapFrom(src => src.Genre.GetNameAttribute()))
                .ForMember(
                    dest => dest.Type,
                    opt => opt.MapFrom(src => src.Type.GetNameAttribute()))
                .ForMember(
                    dest => dest.NumberOfAvailableCopies,
                    opt => opt.MapFrom(src => src.CurrentAvailableNumberOfCopies));

            CreateMap<Author, AuthorResponse>();

            CreateMap<Patron, PatronResponse>();

            CreateMap<BaseCommandResponse, BookReservingResponse>()
                .ConstructUsing(s => new BookReservingResponse(s.Status, s.StatusCode, s.Errors, s.Data));

            CreateMap<BaseCommandResponse, BookAddResponse>()
                .ConstructUsing(s => new BookAddResponse(s.Status, s.StatusCode, s.Errors, s.Data));

            CreateMap<BaseCommandResponse, AuthorAddResponse>()
                .ConstructUsing(s => new AuthorAddResponse(s.Status, s.StatusCode, s.Errors, s.Data));

            CreateMap<BaseCommandResponse, PatronAddResponse>()
                .ConstructUsing(s => new PatronAddResponse(s.Status, s.StatusCode, s.Errors, s.Data));
        }
    }
}
