using Library.Core.Interfaces;
using Library.Core.LibraryAggregate.Specifications;
using Library.SharedKernel.Interfaces;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate.Commands.CheckBookLendings
{
    public class CheckBookLendingsCommandHandler : IRequestHandler<CheckBookLendingsCommand>
    {
        private readonly IRepository<Library> _libraryRepository;
        private readonly IEmailSender _emailSender;

        public CheckBookLendingsCommandHandler(IRepository<Library> libraryRepository, IEmailSender emailSender)
        {
            _libraryRepository = libraryRepository;
            _emailSender = emailSender;
        }

        public async Task<Unit> Handle(CheckBookLendingsCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Content))
                return Unit.Value;
            CheckBookLendingsCommandContent commandContent;
            try
            {
                commandContent = JsonConvert.DeserializeObject<CheckBookLendingsCommandContent>(request.Content);
            }
            catch (Exception) { return Unit.Value; }

            LibraryWithPatronActiveLentBooksSpec libraryWithPatronActiveLentBooksSpec = new LibraryWithPatronActiveLentBooksSpec(commandContent.LibraryId, commandContent.PatronId);
            Library library = await _libraryRepository.GetBySpecAsync(libraryWithPatronActiveLentBooksSpec);
            if (library == null)
                return Unit.Value;

            foreach (var bookLendingId in commandContent.BookLendingsIds)
            {
                if (library.BookLendings.Any(bl => bl.Id == bookLendingId))
                {
                    // Disable Patron.
                    // _emailSender.SendEmailAsync();
                }
            }

            return Unit.Value;
        }
    }
}
