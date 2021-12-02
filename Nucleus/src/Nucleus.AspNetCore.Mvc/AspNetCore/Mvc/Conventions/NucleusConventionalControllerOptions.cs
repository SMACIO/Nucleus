using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Nucleus.Content;
using Nucleus.Http.Modeling;

namespace Nucleus.AspNetCore.Mvc.Conventions
{
    public class NucleusConventionalControllerOptions
    {
        public ConventionalControllerSettingList ConventionalControllerSettings { get; }

        public List<Type> FormBodyBindingIgnoredTypes { get; }

        /// <summary>
        /// Set true to use the old style URL path style.
        /// Default: false.
        /// </summary>
        public bool UseV3UrlStyle { get; set; }

        public NucleusConventionalControllerOptions()
        {
            ConventionalControllerSettings = new ConventionalControllerSettingList();

            FormBodyBindingIgnoredTypes = new List<Type>
            {
                typeof(IFormFile),
                typeof(IRemoteStreamContent)
            };
        }

        public NucleusConventionalControllerOptions Create(
            Assembly assembly,
            [CanBeNull] Action<ConventionalControllerSetting> optionsAction = null)
        {
            var setting = new ConventionalControllerSetting(
                assembly,
                ModuleApiDescriptionModel.DefaultRootPath,
                ModuleApiDescriptionModel.DefaultRemoteServiceName
            );

            optionsAction?.Invoke(setting);
            setting.Initialize();
            ConventionalControllerSettings.Add(setting);
            return this;
        }
    }
}




