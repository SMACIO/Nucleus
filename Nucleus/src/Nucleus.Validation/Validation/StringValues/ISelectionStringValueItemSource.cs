using System.Collections.Generic;

namespace Nucleus.Validation.StringValues
{
    public interface ISelectionStringValueItemSource
    {
        ICollection<ISelectionStringValueItem> Items { get; }
    }
}
