﻿using System;
using System.Collections.Generic;

namespace Nucleus.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending
{
    [Serializable]
    public class ExtensionEnumDto
    {
        public List<ExtensionEnumFieldDto> Fields { get; set; }

        public string LocalizationResource { get; set; }
    }
}
