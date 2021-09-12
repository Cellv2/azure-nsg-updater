using Microsoft.Azure.Management.Network.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureNsgUpdater.Data
{
    public interface INetworkSecurityGroupService
    {
        Task<IPagedCollection<INetworkSecurityGroup>> RetrieveAllNetworkSecurityGroupsAsync();
        Task AddSourceIpAddressToSecurityRuleAsync(INetworkSecurityRule networkSecurityRule, string ipAddress);
        Task AddSourceIpAddressToSecurityRuleAsync(List<INetworkSecurityRule> networkSecurityRules, string ipAddress);
    }
}