using Community.SemanticKernel.Plugins.Collections;
using Community.SemanticKernel.Plugins.OperatingSystem;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Planning.Handlebars;

namespace Community.SemanticKernel.Samples.OperatingSystem;
public static class PluginRunner
{

    public static async Task RunSampleAsync(string goal, bool shouldPrintPrompt = false, params string[] pluginDirectoryNames)
    {
        string apiKey = SampleConfiguration.AzureOpenAI.ApiKey;
        string chatDeploymentName = SampleConfiguration.AzureOpenAI.ChatDeploymentName;
        string chatModelId = SampleConfiguration.AzureOpenAI.ChatModelId;
        string endpoint = SampleConfiguration.AzureOpenAI.Endpoint;

        if (apiKey == null || chatDeploymentName == null || chatModelId == null || endpoint == null)
        {
            Console.WriteLine("Azure endpoint, apiKey, deploymentName, or modelId not found. Skipping example.");
            return;
        }

        var kernel = Kernel.CreateBuilder()
            .AddAzureOpenAIChatCompletion(
                deploymentName: chatDeploymentName,
                endpoint: endpoint,
                serviceId: "AzureOpenAIChat",
                apiKey: apiKey,
                modelId: chatModelId)
            .Build();

        kernel.ImportPluginFromType<OperatingSystemPlugin>();
        kernel.ImportPluginFromType<CollectionPlugin>();

        // Use gpt-4 or newer models if you want to test with loops. 
        // Older models like gpt-35-turbo are less recommended. They do handle loops but are more prone to syntax errors.
        var allowLoopsInPlan = chatDeploymentName.Contains("gpt-4", StringComparison.OrdinalIgnoreCase);
        
        var planner = new HandlebarsPlanner(
            new HandlebarsPlannerOptions()
            {
                // Change this if you want to test with loops regardless of model selection.
                AllowLoops = allowLoopsInPlan
                
            });

        Console.WriteLine($"Goal: {goal}");

        // Create the plan
        var plan = await planner.CreatePlanAsync(kernel, goal);

        // Print the prompt template
        if (shouldPrintPrompt && plan.Prompt is not null)
        {
            Console.WriteLine($"\nPrompt template:\n{plan.Prompt}");
        }

        Console.WriteLine($"\nOriginal plan:\n{plan}");

        // Execute the plan
        var result = await plan.InvokeAsync(kernel);
        Console.WriteLine($"\nResult:\n{result}\n");
    }
}
