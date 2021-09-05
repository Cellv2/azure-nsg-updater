using AzureNsgUpdater.Classes;
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

        private void CreateAzureAppConnection()
        {
            AzureAdSecretDetails azureAdSecretDetails = RetrieveAzureAdSecretConfiguration();

            var servicePrincipal = new ServicePrincipalLoginInformation { ClientId = azureAdSecretDetails.CLIENT_ID, ClientSecret = azureAdSecretDetails.CLIENT_SECRET };
            var creds = new AzureCredentials(servicePrincipal, tenantId: azureAdSecretDetails.TENANT_ID, AzureEnvironment.AzureGlobalCloud); ;
            
            _azureConnection = Microsoft.Azure.Management.Fluent.Azure.Configure()
                .Authenticate(creds)
                .WithSubscription(azureAdSecretDetails.SUBSCRIPTION_ID);
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

        private AzureAdSecretDetails RetrieveAzureAdSecretConfiguration()
        {
            return new AzureAdSecretDetails { TENANT_ID = _configuration["TENANT_ID"], CLIENT_ID = _configuration["CLIENT_ID"], CLIENT_SECRET = _configuration["CLIENT_SECRET"], SUBSCRIPTION_ID = _configuration["SUBSCRIPTION_ID"] };
        }
    }
}
