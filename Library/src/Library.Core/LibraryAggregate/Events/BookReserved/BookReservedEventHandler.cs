using Ardalis.GuardClauses;
using Library.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate.Events.BookReserved
{
    public class BookReservedEventHandler : INotificationHandler<BookReservedEvent>
    {
        private readonly IEmailSender _emailSender;

        public BookReservedEventHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public Task Handle(BookReservedEvent domainEvent, CancellationToken cancellationToken)
        {
            Guard.Against.Null(domainEvent, nameof(domainEvent));

            return _emailSender.SendEmailAsync("test@test.com", "test@test.com", $"{domainEvent.BookReservation.Id} was completed.", domainEvent.BookReservation.ToString());
        }
    }
}
