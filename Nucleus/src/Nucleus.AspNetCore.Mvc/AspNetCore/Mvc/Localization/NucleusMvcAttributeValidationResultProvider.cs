using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Nucleus.AspNetCore.Mvc.Validation;
using Nucleus.DependencyInjection;
using Nucleus.Validation;

namespace Nucleus.AspNetCore.Mvc.Localization
{
    [Dependency(ReplaceServices = true)]
    public class NucleusMvcAttributeValidationResultProvider : DefaultAttributeValidationResultProvider
    {
        private readonly NucleusMvcDataAnnotationsLocalizationOptions _nucleusMvcDataAnnotationsLocalizationOptions;
        private readonly IStringLocalizerFactory _stringLocalizerFactory;

        public NucleusMvcAttributeValidationResultProvider(
            IOptions<NucleusMvcDataAnnotationsLocalizationOptions> nucleusMvcDataAnnotationsLocalizationOptions,
            IStringLocalizerFactory stringLocalizerFactory)
        {
            _nucleusMvcDataAnnotationsLocalizationOptions = nucleusMvcDataAnnotationsLocalizationOptions.Value;
            _stringLocalizerFactory = stringLocalizerFactory;
        }

        public override ValidationResult GetOrDefault(ValidationAttribute validationAttribute, object validatingObject, ValidationContext validationContext)
        {
            var resourceSource = _nucleusMvcDataAnnotationsLocalizationOptions.AssemblyResources.GetOrDefault(validationContext.ObjectType.Assembly);
            if (resourceSource == null)
            {
                return base.GetOrDefault(validationAttribute, validatingObject, validationContext);
            }

            if (validationAttribute.ErrorMessage == null)
            {
                ValidationAttributeHelper.SetDefaultErrorMessage(validationAttribute);
            }

            if (validationAttribute.ErrorMessage != null)
            {
                validationAttribute.ErrorMessage = _stringLocalizerFactory.Create(resourceSource)[validationAttribute.ErrorMessage];
            }

            return base.GetOrDefault(validationAttribute, validatingObject, validationContext);
        }
    }
}







