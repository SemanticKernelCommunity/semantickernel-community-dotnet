// See https://aka.ms/new-console-template for more information
using Community.SemanticKernel.Samples.OperatingSystem;

SampleConfiguration.Initialize();

// await PluginRunner.RunSampleAsync("Get currently available environment variables, foreach variable format output with {Key}:{Value}");
// await PluginRunner.RunSampleAsync("Voir la valeur de la variable d'env PATH");
// await PluginRunner.RunSampleAsync("Get files from the directory '.'");
// await PluginRunner.RunSampleAsync("Get csproj files from the directory '.'");
// await PluginRunner.RunSampleAsync("Get first files from the directory '.'");
// await PluginRunner.RunSampleAsync("Get first files ordered from the directory '.'");
await PluginRunner.RunSampleAsync("Get first file in descending order from the directory '.'");
await PluginRunner.RunSampleAsync("Get last file in ascending order from the directory '.'");

// await PluginRunner.RunSampleAsync("Merge list files from the directory '.' and the directory '..");
// await PluginRunner.RunSampleAsync("Count number of files in the directory '.'");

// await PluginRunner.RunSampleAsync("Append XXX item to the list of files in the directory '.'");

// await PluginRunner.RunSampleAsync("Get the first three items from the directory '.'");

// await PluginRunner.RunSampleAsync("Retrieves the first three elements of the files in the '.' directory, disregarding the first 2 elements.");

await PluginRunner.RunSampleAsync("Reverse the files list from the directory '.'");
