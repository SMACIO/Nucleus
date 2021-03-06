// This file is automatically generated by NUCLEUS framework to use MVC Controllers from CSharp
using System;
using System.Threading.Tasks;
using Nucleus.Application.Dtos;
using Nucleus.Http.Client;
using Nucleus.DependencyInjection;
using Nucleus.Http.Client.ClientProxying;
using Nucleus.AspNetCore.Mvc.ApplicationConfigurations;

// ReSharper disable once CheckNamespace
namespace Nucleus.AspNetCore.Mvc.ApplicationConfigurations.ClientProxies
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(INucleusApplicationConfigurationAppService), typeof(NucleusApplicationConfigurationClientProxy))]
    public partial class NucleusApplicationConfigurationClientProxy : ClientProxyBase<INucleusApplicationConfigurationAppService>, INucleusApplicationConfigurationAppService
    {
        public virtual async Task<ApplicationConfigurationDto> GetAsync()
        {
            return await RequestAsync<ApplicationConfigurationDto>(nameof(GetAsync));
        }
    }
}







