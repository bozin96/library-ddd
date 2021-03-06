using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.SharedKernel
{
    //public abstract class BaseCommand<T> : IRequest<T> where T : BaseCommandResponse
    //{
    //    public Guid AggregateId { get; protected set; }
    //    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    //    public ValidationResult ValidationResult { get; set; }

    //    public BaseCommand()
    //    {
    //        DateOccurred = DateTime.UtcNow;
    //    }

    //    public abstract bool IsValid();
    //}

    public abstract class BaseCommand: IRequest<BaseCommandResponse>
    {
        public Guid AggregateId { get; protected set; }
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
        public ValidationResult ValidationResult { get; set; }

        public BaseCommand()
        {
            DateOccurred = DateTime.UtcNow;
        }

        public abstract bool IsValid();
    }
}
