using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.SharedKernel.Interfaces
{
    public interface IMediatorHandler
    {
        Task<U> SendCommand<T,U>(T command) where T : BaseCommand<U> where U: BaseCommandResponse;
        Task RaiseEvent<T>(T @event) where T : BaseDomainEvent;
    }
}
