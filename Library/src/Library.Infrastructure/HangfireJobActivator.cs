using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure
{
    public class HangfireJobActivator : JobActivator
    {
        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="HangfireHelpers"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public HangfireJobActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        /// <summary>
        /// Activates the job.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public override object ActivateJob(Type type)
        {
            return _serviceProvider.GetService(type);
        }
    }
}
