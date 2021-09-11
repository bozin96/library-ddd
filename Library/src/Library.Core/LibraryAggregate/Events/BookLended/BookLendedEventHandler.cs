using Ardalis.GuardClauses;
using Library.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate.Events.BookLended
{
    public class BookLendedEventHandler : INotificationHandler<BookLentEvent>
    {
        private readonly IEmailSender _emailSender;

        // In a REAL app you might want to use the Outbox pattern and a command/queue here...
        public BookLendedEventHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public Task Handle(BookLentEvent domainEvent, CancellationToken cancellationToken)
        {
            Guard.Against.Null(domainEvent, nameof(domainEvent));

            return _emailSender.SendEmailAsync("test@test.com", "test@test.com", $"{domainEvent.BookLending.Id} was completed.", domainEvent.BookLending.ToString());
        }
    }
}
