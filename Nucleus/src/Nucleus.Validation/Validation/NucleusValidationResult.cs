using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nucleus.Validation
{
    public class NucleusValidationResult : INucleusValidationResult
    {
        public List<ValidationResult> Errors { get; }

        public NucleusValidationResult()
        {
            Errors = new List<ValidationResult>();
        }
    }
}



