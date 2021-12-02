﻿using JetBrains.Annotations;

namespace Nucleus.Modularity
{
    public interface IModuleManager
    {
        void InitializeModules([NotNull] ApplicationInitializationContext context);

        void ShutdownModules([NotNull] ApplicationShutdownContext context);
    }
}

