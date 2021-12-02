using System.ComponentModel.DataAnnotations;

namespace Nucleus.Validation
{
    public interface IAttributeValidationResultProvider
    {
        ValidationResult GetOrDefault(ValidationAttribute validationAttribute, object validatingObject, ValidationContext validationContext);
    }
}

