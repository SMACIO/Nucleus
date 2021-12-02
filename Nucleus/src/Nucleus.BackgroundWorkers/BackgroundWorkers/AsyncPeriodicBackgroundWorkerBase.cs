using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nucleus.ExceptionHandling;
using Nucleus.Threading;

namespace Nucleus.BackgroundWorkers
{
    public abstract class AsyncPeriodicBackgroundWorkerBase : BackgroundWorkerBase
    {
        protected IServiceScopeFactory ServiceScopeFactory { get; }
        protected NucleusAsyncTimer Timer { get; }

        protected AsyncPeriodicBackgroundWorkerBase(
            NucleusAsyncTimer timer,
            IServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = serviceScopeFactory;
            Timer = timer;
            Timer.Elapsed = Timer_Elapsed;
        }

        public override async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await base.StartAsync(cancellationToken);
            Timer.Start(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken = default)
        {
            Timer.Stop(cancellationToken);
            await base.StopAsync(cancellationToken);
        }

        private async Task Timer_Elapsed(NucleusAsyncTimer timer)
        {
            await DoWorkAsync();
        }

        private async Task DoWorkAsync()
        {
            using (var scope = ServiceScopeFactory.CreateScope())
            {
                try
                {
                    await DoWorkAsync(new PeriodicBackgroundWorkerContext(scope.ServiceProvider));
                }
                catch (Exception ex)
                {
                    await scope.ServiceProvider
                        .GetRequiredService<IExceptionNotifier>()
                        .NotifyAsync(new ExceptionNotificationContext(ex));

                    Logger.LogException(ex);
                }
            }
        }

        protected abstract Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext);
    }
}




