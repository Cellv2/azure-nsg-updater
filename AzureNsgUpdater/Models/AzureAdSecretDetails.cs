using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureNsgUpdater.Models
{
    public class AzureAdSecretDetails
    {
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string SubscriptionId { get; set; }
    }
}
