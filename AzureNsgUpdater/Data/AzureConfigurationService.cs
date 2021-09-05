using AzureNsgUpdater.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;

namespace AzureNsgUpdater.Data
{
    public class AzureConfigurationService : IAzureConfigurationService
    {
        private readonly IConfiguration _configuration;
        private Microsoft.Azure.Management.Fluent.IAzure _azureConnection;

        public AzureConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Microsoft.Azure.Management.Fluent.IAzure GetAzureAppConnection()
        {
            if (_azureConnection == null)
            {
                CreateAzureAppConnection();
            }

            return _azureConnection;
        }

        public Microsoft.Azure.Management.Fluent.IAzure RefreshAndReturnNewAzureAppConnection()
        {
            CreateAzureAppConnection();

            return _azureConnection;
        }

        private void CreateAzureAppConnection()
        {
            AzureAdSecretDetails azureAdSecretDetails = RetrieveAzureAdSecretConfiguration();

            var servicePrincipal = new ServicePrincipalLoginInformation { ClientId = azureAdSecretDetails.ClientId, ClientSecret = azureAdSecretDetails.ClientSecret };
            var creds = new AzureCredentials(servicePrincipal, tenantId: azureAdSecretDetails.TenantId, AzureEnvironment.AzureGlobalCloud); ;

            _azureConnection = Microsoft.Azure.Management.Fluent.Azure.Configure()
                .Authenticate(creds)
                .WithSubscription(azureAdSecretDetails.SubscriptionId);
        }

        private AzureAdSecretDetails RetrieveAzureAdSecretConfiguration()
        {
            return new AzureAdSecretDetails { TenantId = _configuration["TENANT_ID"], ClientId = _configuration["CLIENT_ID"], ClientSecret = _configuration["CLIENT_SECRET"], SubscriptionId = _configuration["SUBSCRIPTION_ID"] };
        }
    }
}
