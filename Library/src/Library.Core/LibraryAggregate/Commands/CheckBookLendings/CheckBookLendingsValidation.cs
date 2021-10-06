using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate.Commands.CheckBookLendings
{
    public class CheckBookLendingsValidation : AbstractValidator<CheckBookLendingsCommand>
    {

        //public CheckBookLendingsValidation()
        //{
        //    ValidatePatronId();
        //    ValidateBookLendingsIds();
        //}

        //private void ValidatePatronId()
        //{
        //    RuleFor(c => c.PatronId).NotEqual(Guid.Empty);
        //}

        //private void ValidateBookLendingsIds()
        //{
        //    RuleFor(c => c.BookLendingsIds).NotEmpty();
        //}
    }
}
