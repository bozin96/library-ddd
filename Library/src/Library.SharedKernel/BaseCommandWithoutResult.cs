using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.SharedKernel
{
    public abstract class BaseCommandWithoutResult : IRequest
    {
        public Guid AggregateId { get; protected set; }
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;

        public ValidationResult ValidationResult { get; set; }

        public BaseCommandWithoutResult()
        {

        }

        public BaseCommandWithoutResult(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }

        public abstract bool IsValid();
    }
}
