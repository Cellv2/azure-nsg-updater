namespace AzureNsgUpdater.Data
{
    public interface IAzureConfigurationService
    {
        Microsoft.Azure.Management.Fluent.IAzure GetAzureAppConnection();
        Microsoft.Azure.Management.Fluent.IAzure RefreshAndReturnNewAzureAppConnection();

    }
}