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

    public class AzureOpenAIConfig
    {
        public string? ChatDeploymentName { get; set; }
        public string? ChatModelId { get; set; }
        public string? Endpoint { get; set; }
        public string? ApiKey { get; set; }
    }

}
