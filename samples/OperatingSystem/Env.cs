﻿using Microsoft.Extensions.Configuration;

namespace Community.SemanticKernel.Samples.OperatingSystem;
internal sealed class Env
{
    /// <summary>
    /// Simple helper used to load env vars and secrets like credentials,
    /// to avoid hard coding them in the sample code
    /// </summary>
    /// <param name="name">Secret name / Env var name</param>
    /// <returns>Value found in Secret Manager or Environment Variable</returns>
    internal static string Var(string name)
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<Env>()
            .Build();

        var value = configuration[name];
        if (!string.IsNullOrEmpty(value))
        {
            return value;
        }

        value = Environment.GetEnvironmentVariable(name);
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException($"Secret / Env var not set: {name}");
        }

        return value;
    }
}