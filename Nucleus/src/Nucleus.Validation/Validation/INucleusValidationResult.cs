using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nucleus.Validation
{
    public interface INucleusValidationResult
    {
        List<ValidationResult> Errors { get; }
    }
}

