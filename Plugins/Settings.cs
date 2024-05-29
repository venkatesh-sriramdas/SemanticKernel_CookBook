
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins;

public static class Settings
{
    public static string AoaiEndpoint { get; } // Read-only property
    public static string AoaiApiKey { get; }
    public static string AoaiCompletionDeploymentName { get; }
    public static string AoaiEmbeddingDeploymentName { get; }

    static Settings()
    {
        // Load the configuration values from appsettings.json
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.local.json")
            .Build();

        AoaiEndpoint = configuration["SK_Creds:aoai_endpoint"]!;
        AoaiApiKey = configuration["SK_Creds:aoai_apikey"]!;
        AoaiCompletionDeploymentName = configuration["SK_Creds:aoai_completion_deploymentname"]!;
        AoaiEmbeddingDeploymentName = configuration["SK_Creds:aoai_embedding_deploymentname"]!;
    }
}

