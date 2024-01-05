using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;

namespace Community.SemanticKernel.Samples.OperatingSystem;

public class SampleConfiguration
{
 private readonly IConfigurationRoot _configRoot;
    private static SampleConfiguration? s_instance;

    private SampleConfiguration(IConfigurationRoot configRoot)
    {
        this._configRoot = configRoot;
    }

    public static void Initialize()
    {
         IConfigurationRoot configRoot = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Development.json", true)
            .AddEnvironmentVariables()
            .AddUserSecrets<Env>()
            .Build();

        s_instance = new SampleConfiguration(configRoot);
    }

    public static AzureOpenAIConfig AzureOpenAI => LoadSection<AzureOpenAIConfig>();


    private static T LoadSection<T>([CallerMemberName] string? caller = null)
    {
        if (s_instance == null)
        {
            throw new InvalidOperationException(
                "SampleConfiguration must be initialized with a call to Initialize(IConfigurationRoot) before accessing configuration values.");
        }

        if (string.IsNullOrEmpty(caller))
        {
            throw new ArgumentNullException(nameof(caller));
        }

        return s_instance._configRoot.GetSection(caller).Get<T>() ??
            throw new KeyNotFoundException($"Could not find configuration section {caller}");
    }
    
// dotnet user-secrets set "AzureOpenAI:ServiceId" "..."
// dotnet user-secrets set "AzureOpenAI:DeploymentName" "test-seb"
// dotnet user-secrets set "AzureOpenAI:ModelId" "gpt-35-turbo"
// dotnet user-secrets set "AzureOpenAI:ChatDeploymentName" "gpt-4"
// dotnet user-secrets set "AzureOpenAI:ChatModelId" "gpt-4"
// dotnet user-secrets set "AzureOpenAI:Endpoint" "https://davi-francecentral.openai.azure.com/"
// dotnet user-secrets set "AzureOpenAI:ApiKey" "7b7f3e82a70f41ebacae70f6d9a9a405"

    public class AzureOpenAIConfig
    {
        public string? ChatDeploymentName { get; set; }
        public string? ChatModelId { get; set; }
        public string? Endpoint { get; set; }
        public string? ApiKey { get; set; }
    }

}
