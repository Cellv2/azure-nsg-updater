# AzureNsgUpdater

## Development Secrets

For development, secrets are held using the dotnet secret manager ([docs](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows#how-the-secret-manager-tool-works)). Please see the appsettings.json file for info on which secrets need to be set in the secret manager

## Azure AD Service Principals

Make sure to create an Azure AD App/Service Principal with a role assignment which allows the following:
 - Microsoft.Network/networkSecurityGroups/read
 - Microsoft.Network/networkSecurityGroups/write

Microsoft documentation can be found here - [link](https://docs.microsoft.com/en-gb/azure/active-directory/develop/howto-create-service-principal-portal)
