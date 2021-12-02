using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;
using Nucleus.Uow;

namespace Nucleus.AspNetCore.Uow
{
    public class NucleusUnitOfWorkMiddleware : IMiddleware, ITransientDependency
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly NucleusAspNetCoreUnitOfWorkOptions _options;

        public NucleusUnitOfWorkMiddleware(
            IUnitOfWorkManager unitOfWorkManager,
            IOptions<NucleusAspNetCoreUnitOfWorkOptions> options)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (IsIgnoredUrl(context))
            {
                await next(context);
                return;
            }
            
            using (var uow = _unitOfWorkManager.Reserve(UnitOfWork.UnitOfWorkReservationName))
            {
                await next(context);
                await uow.CompleteAsync(context.RequestAborted);
            }
        }

        private bool IsIgnoredUrl(HttpContext context)
        {
            return context.Request.Path.Value != null &&
                   _options.IgnoredUrls.Any(x => context.Request.Path.Value.StartsWith(x));
        }
    }
}





