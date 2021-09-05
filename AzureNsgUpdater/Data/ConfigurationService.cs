using AzureNsgUpdater.Classes;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureNsgUpdater.Data
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;
        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AzureAdSecretDetails RetrieveAzureAdSecretConfiguration()
        {
            return new AzureAdSecretDetails { TENANT_ID = _configuration["TENANT_ID"], CLIENT_ID = _configuration["CLIENT_ID"], CLIENT_SECRET = _configuration["CLIENT_SECRET"], SUBSCRIPTION_ID = _configuration["SUBSCRIPTION_ID"] };
        }
    }
}
