using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Nucleus.AspNetCore.MultiTenancy;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NucleusOpenIdConnectExtensions
    {
        public static AuthenticationBuilder AddNucleusOpenIdConnect(this AuthenticationBuilder builder)
            => builder.AddNucleusOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, _ => { });

        public static AuthenticationBuilder AddNucleusOpenIdConnect(this AuthenticationBuilder builder, Action<OpenIdConnectOptions> configureOptions)
            => builder.AddNucleusOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddNucleusOpenIdConnect(this AuthenticationBuilder builder, string authenticationScheme, Action<OpenIdConnectOptions> configureOptions)
            => builder.AddNucleusOpenIdConnect(authenticationScheme, OpenIdConnectDefaults.DisplayName, configureOptions);

        public static AuthenticationBuilder AddNucleusOpenIdConnect(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<OpenIdConnectOptions> configureOptions)
        {
            return builder.AddOpenIdConnect(authenticationScheme, displayName, options =>
            {
                options.ClaimActions.MapNucleusClaimTypes();

                configureOptions?.Invoke(options);

                options.Events ??= new OpenIdConnectEvents();
                var authorizationCodeReceived = options.Events.OnAuthorizationCodeReceived ?? (_ => Task.CompletedTask);

                options.Events.OnAuthorizationCodeReceived = receivedContext =>
                {
                    SetNucleusTenantId(receivedContext);
                    return authorizationCodeReceived.Invoke(receivedContext);
                };

                options.Events.OnRemoteFailure = remoteFailureContext =>
                {
                    if (remoteFailureContext.Failure is OpenIdConnectProtocolException &&
                        remoteFailureContext.Failure.Message.Contains("access_denied"))
                    {
                        remoteFailureContext.HandleResponse();
                        remoteFailureContext.Response.Redirect($"{remoteFailureContext.Request.PathBase}/");
                    }
                    return Task.CompletedTask;
                };
            });
        }

        private static void SetNucleusTenantId(AuthorizationCodeReceivedContext receivedContext)
        {
            var tenantKey = receivedContext.HttpContext.RequestServices
                .GetRequiredService<IOptions<NucleusAspNetCoreMultiTenancyOptions>>().Value.TenantKey;

            if (receivedContext.Request.Cookies.ContainsKey(tenantKey))
            {
                receivedContext.TokenEndpointRequest.SetParameter(tenantKey,
                    receivedContext.Request.Cookies[tenantKey]);
            }
        }
    }
}




