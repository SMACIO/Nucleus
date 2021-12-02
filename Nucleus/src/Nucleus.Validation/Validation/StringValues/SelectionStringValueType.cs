﻿using System;

namespace Nucleus.Validation.StringValues
{
    [Serializable]
    [StringValueType("SELECTION")]
    public class SelectionStringValueType : StringValueTypeBase
    {
        public ISelectionStringValueItemSource ItemSource { get; set; }

        public SelectionStringValueType()
        {

        }

        public SelectionStringValueType(IValueValidator validator)
            : base(validator)
        {

        }
    }
}
