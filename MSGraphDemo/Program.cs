using Azure.Core.Pipeline;
using Azure.Identity;
using Microsoft.Graph;

var tenantId = "TENANT_ID";
var clientId = "CLIENT_ID";
var clientSecret = "CLIENT_SECRET";

var handler = new HttpClientHandler
{
};

// Create an options object for the credential being used
// For example, here we're using a ClientSecretCredential so
// we create a ClientSecretCredentialOptions object
var options = new ClientSecretCredentialOptions
{
    // Create a new Azure.Core.HttpClientTransport
    Transport = new HttpClientTransport(handler)
};

var credential = new ClientSecretCredential(
    tenantId,
    clientId,
    clientSecret,
    options
);

var scopes = new[] { "https://graph.microsoft.com/.default" };

// This example works with Microsoft.Graph 4+
var httpClient = GraphClientFactory.Create(
    new TokenCredentialAuthProvider(credential, scopes)
);

var graphClient = new GraphServiceClient(httpClient);

var users = await graphClient.Users.Request().GetAsync();

Console.ReadLine();
