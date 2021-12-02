using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;
using Nucleus.Reflection;

namespace Nucleus.AspNetCore.Mvc.ApiExploring
{
    public class NucleusRemoteServiceApiDescriptionProvider : IApiDescriptionProvider, ITransientDependency
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly MvcOptions _mvcOptions;
        private readonly NucleusRemoteServiceApiDescriptionProviderOptions _options;

        public NucleusRemoteServiceApiDescriptionProvider(
            IModelMetadataProvider modelMetadataProvider,
            IOptions<MvcOptions> mvcOptionsAccessor,
            IOptions<NucleusRemoteServiceApiDescriptionProviderOptions> optionsAccessor)
        {
            _modelMetadataProvider = modelMetadataProvider;
            _mvcOptions = mvcOptionsAccessor.Value;
            _options = optionsAccessor.Value;
        }

        public void OnProvidersExecuted(ApiDescriptionProviderContext context)
        {
        }

        /// <summary>
        /// The order -999 ensures that this provider is executed right after the
        /// Microsoft.AspNetCore.Mvc.ApiExplorer.DefaultApiDescriptionProvider.
        /// </summary>
        public int Order => -999;

        public void OnProvidersExecuting(ApiDescriptionProviderContext context)
        {
            foreach (var apiResponseType in GetApiResponseTypes())
            {
                foreach (var result in context.Results.Where(IsRemoteService))
                {
                    var actionProducesResponseTypeAttributes =
                        ReflectionHelper.GetAttributesOfMemberOrDeclaringType<ProducesResponseTypeAttribute>(
                            result.ActionDescriptor.GetMethodInfo());
                    if (actionProducesResponseTypeAttributes.Any(x => x.StatusCode == apiResponseType.StatusCode))
                    {
                        continue;
                    }

                    result.SupportedResponseTypes.AddIfNotContains(x => x.StatusCode == apiResponseType.StatusCode,
                        () => apiResponseType);
                }
            }
        }

        protected virtual IEnumerable<ApiResponseType> GetApiResponseTypes()
        {
            foreach (var apiResponse in _options.SupportedResponseTypes)
            {
                apiResponse.ModelMetadata = _modelMetadataProvider.GetMetadataForType(apiResponse.Type);

                foreach (var responseTypeMetadataProvider in _mvcOptions.OutputFormatters.OfType<IApiResponseTypeMetadataProvider>())
                {
                    var formatterSupportedContentTypes = responseTypeMetadataProvider.GetSupportedContentTypes(null, apiResponse.Type);
                    if (formatterSupportedContentTypes == null)
                    {
                        continue;
                    }

                    foreach (var formatterSupportedContentType in formatterSupportedContentTypes)
                    {
                        apiResponse.ApiResponseFormats.Add(new ApiResponseFormat
                        {
                            Formatter = (IOutputFormatter) responseTypeMetadataProvider,
                            MediaType = formatterSupportedContentType
                        });
                    }
                }
            }

            return _options.SupportedResponseTypes;
        }

        protected virtual bool IsRemoteService(ApiDescription actionDescriptor)
        {
            if (actionDescriptor.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                var remoteServiceAttr = ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<RemoteServiceAttribute>(controllerActionDescriptor.MethodInfo);
                if (remoteServiceAttr != null && remoteServiceAttr.IsEnabled)
                {
                    return true;
                }

                remoteServiceAttr = ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<RemoteServiceAttribute>(controllerActionDescriptor.ControllerTypeInfo);
                if (remoteServiceAttr != null && remoteServiceAttr.IsEnabled)
                {
                    return true;
                }

                if (typeof(IRemoteService).IsAssignableFrom(controllerActionDescriptor.ControllerTypeInfo))
                {
                    return true;
                }
            }

            return false;
        }
    }
}





