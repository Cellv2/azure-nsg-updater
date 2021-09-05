using AzureNsgUpdater.Classes;

namespace AzureNsgUpdater.Data
{
    public interface IConfigurationService
    {
        AzureAdSecretDetails RetrieveAzureAdSecretConfiguration();
    }
}