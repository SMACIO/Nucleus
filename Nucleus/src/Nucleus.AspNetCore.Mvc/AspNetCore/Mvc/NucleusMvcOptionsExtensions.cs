using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.AspNetCore.Mvc.Auditing;
using Nucleus.AspNetCore.Mvc.ContentFormatters;
using Nucleus.AspNetCore.Mvc.Conventions;
using Nucleus.AspNetCore.Mvc.ExceptionHandling;
using Nucleus.AspNetCore.Mvc.Features;
using Nucleus.AspNetCore.Mvc.GlobalFeatures;
using Nucleus.AspNetCore.Mvc.ModelBinding;
using Nucleus.AspNetCore.Mvc.Response;
using Nucleus.AspNetCore.Mvc.Uow;
using Nucleus.AspNetCore.Mvc.Validation;
using Nucleus.Content;

namespace Nucleus.AspNetCore.Mvc
{
    internal static class NucleusMvcOptionsExtensions
    {
        public static void AddNucleus(this MvcOptions options, IServiceCollection services)
        {
            AddConventions(options, services);
            AddActionFilters(options);
            AddPageFilters(options);
            AddModelBinders(options);
            AddMetadataProviders(options, services);
            AddFormatters(options);
        }

        private static void AddFormatters(MvcOptions options)
        {
            options.OutputFormatters.Insert(0, new RemoteStreamContentOutputFormatter());
        }

        private static void AddConventions(MvcOptions options, IServiceCollection services)
        {
            options.Conventions.Add(new NucleusServiceConventionWrapper(services));
        }

        private static void AddActionFilters(MvcOptions options)
        {
            options.Filters.AddService(typeof(GlobalFeatureActionFilter));
            options.Filters.AddService(typeof(NucleusAuditActionFilter));
            options.Filters.AddService(typeof(NucleusNoContentActionFilter));
            options.Filters.AddService(typeof(NucleusFeatureActionFilter));
            options.Filters.AddService(typeof(NucleusValidationActionFilter));
            options.Filters.AddService(typeof(NucleusUowActionFilter));
            options.Filters.AddService(typeof(NucleusExceptionFilter));
        }

        private static void AddPageFilters(MvcOptions options)
        {
            options.Filters.AddService(typeof(GlobalFeaturePageFilter));
            options.Filters.AddService(typeof(NucleusExceptionPageFilter));
            options.Filters.AddService(typeof(NucleusAuditPageFilter));
            options.Filters.AddService(typeof(NucleusFeaturePageFilter));
            options.Filters.AddService(typeof(NucleusUowPageFilter));
        }

        private static void AddModelBinders(MvcOptions options)
        {
            options.ModelBinderProviders.Insert(0, new NucleusDateTimeModelBinderProvider());
            options.ModelBinderProviders.Insert(1, new NucleusExtraPropertiesDictionaryModelBinderProvider());
            options.ModelBinderProviders.Insert(2, new NucleusRemoteStreamContentModelBinderProvider());
        }

        private static void AddMetadataProviders(MvcOptions options, IServiceCollection services)
        {
            options.ModelMetadataDetailsProviders.Add(new NucleusDataAnnotationAutoLocalizationMetadataDetailsProvider(services));

            options.ModelMetadataDetailsProviders.Add(new BindingSourceMetadataProvider(typeof(IRemoteStreamContent), BindingSource.FormFile));
            options.ModelMetadataDetailsProviders.Add(new BindingSourceMetadataProvider(typeof(IEnumerable<IRemoteStreamContent>), BindingSource.FormFile));
            options.ModelMetadataDetailsProviders.Add(new BindingSourceMetadataProvider(typeof(RemoteStreamContent), BindingSource.FormFile));
            options.ModelMetadataDetailsProviders.Add(new BindingSourceMetadataProvider(typeof(IEnumerable<RemoteStreamContent>), BindingSource.FormFile));
            options.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(IRemoteStreamContent)));
            options.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(RemoteStreamContent)));
        }
    }
}





