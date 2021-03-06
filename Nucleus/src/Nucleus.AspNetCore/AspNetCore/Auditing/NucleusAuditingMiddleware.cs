using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Nucleus.Auditing;
using Nucleus.DependencyInjection;
using Nucleus.Uow;
using Nucleus.Users;

namespace Nucleus.AspNetCore.Auditing
{
    public class NucleusAuditingMiddleware : IMiddleware, ITransientDependency
    {
        private readonly IAuditingManager _auditingManager;
        protected NucleusAuditingOptions AuditingOptions { get; }
        protected NucleusAspNetCoreAuditingOptions AspNetCoreAuditingOptions { get; }
        protected ICurrentUser CurrentUser { get; }
        protected IUnitOfWorkManager UnitOfWorkManager { get; }

        public NucleusAuditingMiddleware(
            IAuditingManager auditingManager,
            ICurrentUser currentUser,
            IOptions<NucleusAuditingOptions> auditingOptions,
            IOptions<NucleusAspNetCoreAuditingOptions> aspNetCoreAuditingOptions,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _auditingManager = auditingManager;

            CurrentUser = currentUser;
            UnitOfWorkManager = unitOfWorkManager;
            AuditingOptions = auditingOptions.Value;
            AspNetCoreAuditingOptions = aspNetCoreAuditingOptions.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (!AuditingOptions.IsEnabled || IsIgnoredUrl(context))
            {
                await next(context);
                return;
            }

            var hasError = false;
            using (var saveHandle = _auditingManager.BeginScope())
            {
                Debug.Assert(_auditingManager.Current != null);

                try
                {
                    await next(context);

                    if (_auditingManager.Current.Log.Exceptions.Any())
                    {
                        hasError = true;
                    }
                }
                catch (Exception ex)
                {
                    hasError = true;

                    if (!_auditingManager.Current.Log.Exceptions.Contains(ex))
                    {
                        _auditingManager.Current.Log.Exceptions.Add(ex);
                    }

                    throw;
                }
                finally
                {
                    if (ShouldWriteAuditLog(context, hasError))
                    {
                        if (UnitOfWorkManager.Current != null)
                        {
                            await UnitOfWorkManager.Current.SaveChangesAsync();
                        }

                        await saveHandle.SaveAsync();
                    }
                }
            }
        }

        private bool IsIgnoredUrl(HttpContext context)
        {
            return context.Request.Path.Value != null &&
                   AspNetCoreAuditingOptions.IgnoredUrls.Any(x => context.Request.Path.Value.StartsWith(x));
        }

        private bool ShouldWriteAuditLog(HttpContext httpContext, bool hasError)
        {
            if (AuditingOptions.AlwaysLogOnException && hasError)
            {
                return true;
            }

            if (!AuditingOptions.IsEnabledForAnonymousUsers && !CurrentUser.IsAuthenticated)
            {
                return false;
            }

            if (!AuditingOptions.IsEnabledForGetRequests &&
                string.Equals(httpContext.Request.Method, HttpMethods.Get, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }
    }
}





