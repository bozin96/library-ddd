using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.SharedKernel.Interfaces
{
    public interface IBaseCommandHandler<TCommand, TOutput> where TCommand : BaseCommand<TOutput> where TOutput : BaseCommandResponse
    {
        TOutput Handle(TCommand command);
    }
}
