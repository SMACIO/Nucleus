using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nucleus.Validation;

namespace Nucleus.AspNetCore.Mvc.Validation
{
    public interface IModelStateValidator
    {
        void Validate(ModelStateDictionary modelState);

        void AddErrors(INucleusValidationResult validationResult, ModelStateDictionary modelState);
    }
}


