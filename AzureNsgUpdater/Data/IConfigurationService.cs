namespace AzureNsgUpdater.Data
{
    public interface IConfigurationService
    {
        Microsoft.Azure.Management.Fluent.IAzure GetAzureAppConnection();
        Microsoft.Azure.Management.Fluent.IAzure RefreshAndReturnNewAzureAppConnection();

    }
}