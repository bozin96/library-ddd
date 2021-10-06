using Hangfire;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.SharedKernel.Extensions
{
    public static class MediatorExtensions
    {
        public static void Enqueue(this IMediator mediator, IRequest request)
        {
            var backgroundJobClient = new BackgroundJobClient();
            backgroundJobClient.Enqueue<MediatorHangfireBridge>(x => x.Send(request));
        }

        public static void Schedule(this IMediator mediator, IRequest request, TimeSpan delay)
        {
            var backgroundJobClient = new BackgroundJobClient();
            backgroundJobClient.Schedule<MediatorHangfireBridge>(x => x.Send(request), delay);
        }
    }
}
