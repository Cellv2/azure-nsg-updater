using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Azure.Management.Network.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using AzureNsgUpdater.Classes;

namespace AzureNsgUpdater.Data
{
    public class NetworkSecurityGroupService
    {
        private readonly IAzureAdSecretDetails _azureAdSecretDetails;

        public NetworkSecurityGroupService(IAzureAdSecretDetails azureAdSecretDetails)
        {
            _azureAdSecretDetails = azureAdSecretDetails;
        }

        public async Task<IPagedCollection<INetworkSecurityGroup>> RetrieveAllNetworkSecurityGroupsAsync()
        {

            var servicePrincipal = new ServicePrincipalLoginInformation { ClientId = _azureAdSecretDetails.CLIENT_ID, ClientSecret = _azureAdSecretDetails.CLIENT_SECRET };
            var creds = new AzureCredentials(servicePrincipal, tenantId: _azureAdSecretDetails.TENANT_ID, AzureEnvironment.AzureGlobalCloud); ;
            var azure = Microsoft.Azure.Management.Fluent.Azure.Configure()
                .Authenticate(creds)
                .WithSubscription(_azureAdSecretDetails.SUBSCRIPTION_ID);


            // example for resourceGroups: https://docs.microsoft.com/en-us/dotnet/api/overview/azure/resource-manager
            var nsgs = await azure.NetworkSecurityGroups.ListAsync();
            return nsgs;
        }
    }
}
