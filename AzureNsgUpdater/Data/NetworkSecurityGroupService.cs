using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Azure.Management.Network.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace AzureNsgUpdater.Data
{
    // https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.management.fluent.azure?view=azure-dotnet
    public class NetworkSecurityGroupService : INetworkSecurityGroupService
    {
        private readonly Microsoft.Azure.Management.Fluent.IAzure _azure;

        public NetworkSecurityGroupService(IAzureConfigurationService azureConfigurationService)
        {
            _azure = azureConfigurationService.GetAzureAppConnection();
        }

        public async Task<IPagedCollection<INetworkSecurityGroup>> RetrieveAllNetworkSecurityGroupsAsync()
        {
            // example for resourceGroups: https://docs.microsoft.com/en-us/dotnet/api/overview/azure/resource-manager
            var nsgs = await _azure.NetworkSecurityGroups.ListAsync();
            return nsgs;
        }

        public async Task AddSourceIpAddressToSecurityRuleAsync(INetworkSecurityRule networkSecurityRule, string ipAddress)
        {
            var networkSecurityGroupToUpdate = await _azure.NetworkSecurityGroups.GetByIdAsync(networkSecurityRule.Parent.Id);
            // TODO: add logic for FromAddress vs FromAddresses, as well as merge IPs as required
            //networkSecurityGroupToUpdate.Update().UpdateRule(networkSecurityRule.Name).FromAddress("192.168.19.30");
            networkSecurityGroupToUpdate.Update().UpdateRule(networkSecurityRule.Name).FromAddress(ipAddress);
            await networkSecurityGroupToUpdate.Update().ApplyAsync();
        }

        public async Task AddSourceIpAddressToSecurityRuleAsync(List<INetworkSecurityRule> networkSecurityRules, string ipAddress)
        {
            foreach (var networkSecurityRule in networkSecurityRules)
            {
                try
                {
                    await AddSourceIpAddressToSecurityRuleAsync(networkSecurityRule, ipAddress);
                } catch (Exception ex)
                {
                    // TODO: fix up error logging
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
