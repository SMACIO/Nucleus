﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;
using Nucleus.DistributedLocking;
using Nucleus.EventBus.Distributed;
using Nucleus.Threading;

namespace Nucleus.EventBus.Boxes
{
    public class OutboxSender : IOutboxSender, ITransientDependency
    {
        protected IServiceProvider ServiceProvider { get; }
        protected NucleusAsyncTimer Timer { get; }
        protected IDistributedEventBus DistributedEventBus { get; }
        protected INucleusDistributedLock DistributedLock { get; }
        protected IEventOutbox Outbox { get; private set; }
        protected OutboxConfig OutboxConfig { get; private set; }
        protected NucleusEventBusBoxesOptions EventBusBoxesOptions { get; }
        protected string DistributedLockName => "NucleusOutbox_" + OutboxConfig.Name;
        public ILogger<OutboxSender> Logger { get; set; }

        protected CancellationTokenSource StoppingTokenSource { get; }
        protected CancellationToken StoppingToken { get; }

        public OutboxSender(
            IServiceProvider serviceProvider,
            NucleusAsyncTimer timer,
            IDistributedEventBus distributedEventBus,
            INucleusDistributedLock distributedLock,
           IOptions<NucleusEventBusBoxesOptions> eventBusBoxesOptions)
        {
            ServiceProvider = serviceProvider;
            DistributedEventBus = distributedEventBus;
            DistributedLock = distributedLock;
            EventBusBoxesOptions = eventBusBoxesOptions.Value;
            Timer = timer;
            Timer.Period = Convert.ToInt32(EventBusBoxesOptions.PeriodTimeSpan.TotalMilliseconds);
            Timer.Elapsed += TimerOnElapsed;
            Logger = NullLogger<OutboxSender>.Instance;
            StoppingTokenSource = new CancellationTokenSource();
            StoppingToken = StoppingTokenSource.Token;
        }

        public virtual Task StartAsync(OutboxConfig outboxConfig, CancellationToken cancellationToken = default)
        {
            OutboxConfig = outboxConfig;
            Outbox = (IEventOutbox)ServiceProvider.GetRequiredService(outboxConfig.ImplementationType);
            Timer.Start(cancellationToken);
            return Task.CompletedTask;
        }

        public virtual Task StopAsync(CancellationToken cancellationToken = default)
        {
            StoppingTokenSource.Cancel();
            Timer.Stop(cancellationToken);
            StoppingTokenSource.Dispose();
            return Task.CompletedTask;
        }

        private async Task TimerOnElapsed(NucleusAsyncTimer arg)
        {
            await RunAsync();
        }

        protected virtual async Task RunAsync()
        {
            await using (var handle = await DistributedLock.TryAcquireAsync(DistributedLockName, cancellationToken: StoppingToken))
            {
                if (handle != null)
                {
                    while (true)
                    {
                        var waitingEvents = await Outbox.GetWaitingEventsAsync(EventBusBoxesOptions.OutboxWaitingEventMaxCount, StoppingToken);
                        if (waitingEvents.Count <= 0)
                        {
                            break;
                        }

                        Logger.LogInformation($"Found {waitingEvents.Count} events in the outbox.");

                        foreach (var waitingEvent in waitingEvents)
                        {
                            await DistributedEventBus
                                .AsSupportsEventBoxes()
                                .PublishFromOutboxAsync(
                                    waitingEvent,
                                    OutboxConfig
                                );

                            await Outbox.DeleteAsync(waitingEvent.Id);
                            Logger.LogInformation($"Sent the event to the message broker with id = {waitingEvent.Id:N}");
                        }
                    }
                }
                else
                {
                    Logger.LogDebug("Could not obtain the distributed lock: " + DistributedLockName);
                    try
                    {
                        await Task.Delay(EventBusBoxesOptions.DistributedLockWaitDuration, StoppingToken);
                    }
                    catch (TaskCanceledException) { }
                }
            }
        }
    }
}






