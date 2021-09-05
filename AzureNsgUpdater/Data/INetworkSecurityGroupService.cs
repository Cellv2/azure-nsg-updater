using Microsoft.Azure.Management.Network.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System.Threading.Tasks;

namespace AzureNsgUpdater.Data
{
    public interface INetworkSecurityGroupService
    {
        Task<IPagedCollection<INetworkSecurityGroup>> RetrieveAllNetworkSecurityGroupsAsync();
        void RetireveNetworkSecurityGroupRules();
    }
}