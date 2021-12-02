using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nucleus.DependencyInjection;
using Nucleus.Validation;

namespace Nucleus.AspNetCore.Mvc.Validation
{
    public class ModelStateValidator : IModelStateValidator, ITransientDependency
    {
        public virtual void Validate(ModelStateDictionary modelState)
        {
            var validationResult = new NucleusValidationResult();

            AddErrors(validationResult, modelState);

            if (validationResult.Errors.Any())
            {
                throw new NucleusValidationException(
                    "ModelState is not valid! See ValidationErrors for details.",
                    validationResult.Errors
                );
            }
        }

        public virtual void AddErrors(INucleusValidationResult validationResult, ModelStateDictionary modelState)
        {
            if (modelState.IsValid)
            {
                return;
            }

            foreach (var state in modelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    validationResult.Errors.Add(new ValidationResult(error.ErrorMessage, new[] { state.Key }));
                }
            }
        }
    }
}



