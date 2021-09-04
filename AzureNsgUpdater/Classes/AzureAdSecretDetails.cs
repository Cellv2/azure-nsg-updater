using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureNsgUpdater.Classes
{
    public class AzureAdSecretDetails : IAzureAdSecretDetails
    {
        public string TENANT_ID { get; set; }
        public string CLIENT_ID { get; set; }
        public string CLIENT_SECRET { get; set; }
        public string SUBSCRIPTION_ID { get; set; }
    }
}
