using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nucleus.Validation
{
    public interface IHasValidationErrors
    {
        IList<ValidationResult> ValidationErrors { get; }
    }
}
