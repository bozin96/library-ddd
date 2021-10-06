using Ardalis.GuardClauses;
using Library.Core.Interfaces;
using Library.Core.SyncedAggregate;
using Library.SharedKernel.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate.Events.BookLended
{
    public class BookLentEventHandler : INotificationHandler<BookLentEvent>
    {
        private readonly IRepository<Patron> _patronRepository;
        private readonly IEmailSender _emailSender;

        public BookLentEventHandler(IEmailSender emailSender, IRepository<Patron> patronRepository)
        {
            _emailSender = emailSender;
            _patronRepository = patronRepository;
        }
        public async Task Handle(BookLentEvent domainEvent, CancellationToken cancellationToken)
        {
            Guard.Against.Null(domainEvent, nameof(domainEvent));
            if (domainEvent?.PatronId != Guid.Empty)
            {
                var patron = await _patronRepository.GetByIdAsync(domainEvent.PatronId);
                if (patron != null)
                    await _emailSender.SendEmailAsync(patron.Email, "test@test.com", $"Book Lending was completed.", $"Book with id {domainEvent.BookId} was lent.");
            }
        }
    }
}
