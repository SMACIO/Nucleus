﻿using JetBrains.Annotations;

namespace Nucleus.ObjectExtending.Modularity
{
    public class ExtensionPropertyUiConfiguration
    {
        [NotNull]
        public ExtensionPropertyUiTableConfiguration OnTable { get; }

        [NotNull]
        public ExtensionPropertyUiFormConfiguration OnCreateForm { get; }

        [NotNull]
        public ExtensionPropertyUiFormConfiguration OnEditForm { get; }

        [NotNull]
        public ExtensionPropertyLookupConfiguration Lookup { get; set; }

        public ExtensionPropertyUiConfiguration()
        {
            OnTable = new ExtensionPropertyUiTableConfiguration();
            OnCreateForm = new ExtensionPropertyUiFormConfiguration();
            OnEditForm = new ExtensionPropertyUiFormConfiguration();
            Lookup = new ExtensionPropertyLookupConfiguration();
        }
    }
}
