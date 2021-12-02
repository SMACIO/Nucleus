using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;
using Nucleus.DynamicProxy;

namespace Nucleus.Uow
{
    public class UnitOfWorkInterceptor : NucleusInterceptor, ITransientDependency
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UnitOfWorkInterceptor(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override async Task InterceptAsync(INucleusMethodInvocation invocation)
        {
            if (!UnitOfWorkHelper.IsUnitOfWorkMethod(invocation.Method, out var unitOfWorkAttribute))
            {
                await invocation.ProceedAsync();
                return;
            }

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var options = CreateOptions(scope.ServiceProvider, invocation, unitOfWorkAttribute);

                var unitOfWorkManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();

                //Trying to begin a reserved UOW by NucleusUnitOfWorkMiddleware
                if (unitOfWorkManager.TryBeginReserved(UnitOfWork.UnitOfWorkReservationName, options))
                {
                    await invocation.ProceedAsync();
                    
                    if (unitOfWorkManager.Current != null)
                    {
                        await unitOfWorkManager.Current.SaveChangesAsync();
                    }

                    return;
                }

                using (var uow = unitOfWorkManager.Begin(options))
                {
                    await invocation.ProceedAsync();
                    await uow.CompleteAsync();
                }
            }
        }

        private NucleusUnitOfWorkOptions CreateOptions(IServiceProvider serviceProvider, INucleusMethodInvocation invocation, [CanBeNull] UnitOfWorkAttribute unitOfWorkAttribute)
        {
            var options = new NucleusUnitOfWorkOptions();

            unitOfWorkAttribute?.SetOptions(options);

            if (unitOfWorkAttribute?.IsTransactional == null)
            {
                var defaultOptions = serviceProvider.GetRequiredService<IOptions<NucleusUnitOfWorkDefaultOptions>>().Value;
                options.IsTransactional = defaultOptions.CalculateIsTransactional(
                    autoValue: serviceProvider.GetRequiredService<IUnitOfWorkTransactionBehaviourProvider>().IsTransactional
                               ?? !invocation.Method.Name.StartsWith("Get", StringComparison.InvariantCultureIgnoreCase)
                );
            }

            return options;
        }
    }
}








