﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;
using Nucleus.Http.Modeling;

namespace Nucleus.Http.ProxyScripting.Generators.JQuery
{
    //TODO: Add nucleus.ajax, nucleus.utils... etc.

    public class JQueryProxyScriptGenerator : IProxyScriptGenerator, ITransientDependency
    {
        //TODO: Should move this to Ddd package by adding an option to NucleusHttpAbstractions module. Also duplicated of ApplicationService.CommonPostfixes
        private static string[] AppServiceCommonPostfixes { get; } = { "AppService", "ApplicationService", "Service" };

        /// <summary>
        /// "jquery".
        /// </summary>
        public const string Name = "jquery";

        private readonly DynamicJavaScriptProxyOptions _dynamicJavaScriptProxyOptions;

        public JQueryProxyScriptGenerator(IOptions<DynamicJavaScriptProxyOptions> dynamicJavaScriptProxyOptions)
        {
            _dynamicJavaScriptProxyOptions = dynamicJavaScriptProxyOptions.Value;
        }

        public string CreateScript(ApplicationApiDescriptionModel model)
        {
            var script = new StringBuilder();

            script.AppendLine("/* This file is automatically generated by NUCLEUS framework to use MVC Controllers from javascript. */");
            script.AppendLine();

            foreach (var module in model.Modules)
            {
                if (!ShouldCreateModuleScript(module))
                {
                    continue;
                }

                script.AppendLine();
                AddModuleScript(script, module.Value);
            }

            AddInitializedEventTrigger(script);

            return script.ToString();
        }

        private bool ShouldCreateModuleScript(KeyValuePair<string, ModuleApiDescriptionModel> module)
        {
            if (_dynamicJavaScriptProxyOptions.DisabledModules.Contains(module.Key))
            {
                return false;
            }

            return true;
        }

        private static void AddModuleScript(StringBuilder script, ModuleApiDescriptionModel module)
        {
            //TODO: Eleminate repeating module.RootPath.Replace("/", ".").ToCamelCase() !
            //TODO: Remove illegal chars (like '-') from module/controller names!

            script.AppendLine($"// module {module.RootPath.ToCamelCase()}");
            script.AppendLine();
            script.AppendLine("(function(){");

            foreach (var controller in module.Controllers.Values)
            {
                script.AppendLine();
                AddControllerScript(script, controller);
            }

            script.AppendLine();
            script.AppendLine("})();");
        }

        private static void AddControllerScript(StringBuilder script, ControllerApiDescriptionModel controller)
        {
            var controllerName = GetNormalizedTypeName(controller.Type);

            script.AppendLine($"  // controller {controllerName}");
            script.AppendLine();
            script.AppendLine("  (function(){");
            script.AppendLine();

            script.AppendLine($"    nucleus.utils.createNamespace(window, '{controllerName}');");

            var normalizedActionNames = CalculateNormalizedActionNames(controller.Actions);

            foreach (var action in controller.Actions.Values)
            {
                script.AppendLine();
                AddActionScript(script, controllerName, action, normalizedActionNames[action]);
            }

            script.AppendLine();
            script.AppendLine("  })();");
        }

        private static void AddActionScript(StringBuilder script, string controllerName, ActionApiDescriptionModel action, string normalizedActionName)
        {
            var parameterList = ProxyScriptingJsFuncHelper.GenerateJsFuncParameterList(action, "ajaxParams");

            script.AppendLine($"    {controllerName}{ProxyScriptingJsFuncHelper.WrapWithBracketsOrWithDotPrefix(normalizedActionName.RemovePostFix("Async").ToCamelCase())} = function({parameterList}) {{");

            var versionParam = action.Parameters.FirstOrDefault(p => p.Name == "apiVersion" && p.BindingSourceId == ParameterBindingSources.Path) ??
                               action.Parameters.FirstOrDefault(p => p.Name == "api-version" && p.BindingSourceId == ParameterBindingSources.Query);

            if (versionParam != null)
            {
                var version = FindBestApiVersion(action);
                script.AppendLine($"      var {ProxyScriptingJsFuncHelper.NormalizeJsVariableName(versionParam.Name)} = '{version}';");
            }

            script.AppendLine("      return nucleus.ajax($.extend(true, {");

            AddAjaxCallParameters(script, action);

            var ajaxParamsIsFromForm = action.Parameters.Any(x => x.BindingSourceId == ParameterBindingSources.Form);
            var dataType = action.ReturnValue.Type == ReturnValueApiDescriptionModel.Create(typeof(string)).Type
                ? "{ dataType: 'text' }, "
                : string.Empty;
            script.AppendLine(ajaxParamsIsFromForm
                ? "      }, $.extend(true, {}, " + dataType + "{ contentType: 'application/x-www-form-urlencoded; charset=UTF-8' }, ajaxParams)));"
                : "      }, " + dataType + "ajaxParams));");

            script.AppendLine("    };");
        }

        private static string FindBestApiVersion(ActionApiDescriptionModel action)
        {
            //var configuredVersion = GetConfiguredApiVersion(); //TODO: Implement
            string configuredVersion = null;

            if (action.SupportedVersions.IsNullOrEmpty())
            {
                return configuredVersion ?? "1.0";
            }

            if (action.SupportedVersions.Contains(configuredVersion))
            {
                return configuredVersion;
            }

            return action.SupportedVersions.Last(); //TODO: Ensure to get the latest version!
        }

        private static void AddAjaxCallParameters(StringBuilder script, ActionApiDescriptionModel action)
        {
            var httpMethod = action.HttpMethod?.ToUpperInvariant() ?? "POST";

            script.AppendLine("        url: nucleus.appPath + '" + ProxyScriptingHelper.GenerateUrlWithParameters(action) + "',");
            script.Append("        type: '" + httpMethod + "'");

            if (action.ReturnValue.Type == typeof(void).FullName)
            {
                script.AppendLine(",");
                script.Append("        dataType: null");
            }

            var headers = ProxyScriptingHelper.GenerateHeaders(action, 8);
            if (headers != null)
            {
                script.AppendLine(",");
                script.Append("        headers: " + headers);
            }

            var body = ProxyScriptingHelper.GenerateBody(action);
            if (!body.IsNullOrEmpty())
            {
                script.AppendLine(",");
                script.Append("        data: JSON.stringify(" + body + ")");
            }
            else
            {
                var formData = ProxyScriptingHelper.GenerateFormPostData(action, 8);
                if (!formData.IsNullOrEmpty())
                {
                    script.AppendLine(",");
                    script.Append("        data: " + formData);
                }
            }

            script.AppendLine();
        }

        private static Dictionary<ActionApiDescriptionModel, string> CalculateNormalizedActionNames(Dictionary<string, ActionApiDescriptionModel> actions)
        {
            var result = new Dictionary<ActionApiDescriptionModel, string>();

            var actionsByName = new Dictionary<string, List<ActionApiDescriptionModel>>();

            foreach (var action in actions.Values)
            {
                var actionName = action.Name.RemovePostFix("Async").ToCamelCase();
                result[action] = actionName;
                actionsByName.GetOrAdd(actionName, () => new List<ActionApiDescriptionModel>()).Add(action);
            }

            foreach (var actionByName in actionsByName)
            {
                if (actionByName.Value.Count <= 1)
                {
                    continue;
                }

                foreach (var action in actionByName.Value)
                {
                    result[action] = action.UniqueName;
                }
            }

            return result;
        }

        private static void AddInitializedEventTrigger(StringBuilder script)
        {
            script.AppendLine();
            script.AppendLine("nucleus.event.trigger('nucleus.serviceProxyScriptInitialized');");
        }

        private static string GetNormalizedTypeName(string typeWithAssemblyName)
        {
            return CamelCaseWithNamespace(
                typeWithAssemblyName.Split(",")[0]
                    .Trim()
                    .RemovePostFix(AppServiceCommonPostfixes)
                    .RemovePostFix("Controller")
                    .Replace(".ClientProxies", string.Empty)
                    .RemovePostFix("ClientProxy")
            );
        }

        private static string CamelCaseWithNamespace(string name)
        {
            return name.Split('.').Select(n => n.ToCamelCase()).JoinAsString(".");
        }
    }
}







