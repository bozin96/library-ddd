using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.SharedKernel.Interfaces
{
    public interface IMediatorHandler
    {
        Task<BaseCommandResponse> SendCommand<T>(T command) where T : BaseCommand;

        Task RaiseEvent<T>(T @event) where T : BaseDomainEvent;

        void ScheduleCommand(BaseCommandWithoutResult command, TimeSpan delay);
    }
}
