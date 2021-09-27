using FluentValidation;
using System;

namespace Library.Core.LibraryAggregate.Commands.AddBooks
{
    class AddBooksValidation : AbstractValidator<AddBooksCommand>
    {
        private static readonly string ISBNPattern = @"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$";

        public AddBooksValidation()
        {
            ValidateTitle();
            ValidateAuthorIdId();
            ValidateISBN();
        }

        private void ValidateTitle()
        {
            RuleFor(c => c.Title).NotEmpty().WithMessage("Title is required.").Length(1, 600)
                .WithMessage("Name must have between 1 and 60 characters.");
        }

        private void ValidateAuthorIdId()
        {
            RuleFor(c => c.AuthorId).NotEqual(Guid.Empty);
        }

        private void ValidateISBN()
        {
            RuleFor(c => c.ISBN).Matches(ISBNPattern);
        }

    }
}
