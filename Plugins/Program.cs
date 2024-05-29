
using static System.Console;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Kernel = Microsoft.SemanticKernel.Kernel;
using Dumpify;
using Plugins.Data;
using Plugins.KernelPlugins;
using Microsoft.SemanticKernel.Plugins.Core;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.Extensions.Logging;


namespace Plugins;
internal class Program
{
    private static readonly IChatCompletionService chat;

    public static Kernel kernel { get; set; }


    static Program()
    {
        var builder = Kernel.CreateBuilder();
        builder.Services.AddSingleton(typeof(Employee))
            .AddLogging(c => c.AddConsole().SetMinimumLevel(LogLevel.None));

        kernel = builder
           .AddAzureOpenAIChatCompletion(Settings.AoaiCompletionDeploymentName, Settings.AoaiEndpoint, Settings.AoaiApiKey)
           .AddAzureOpenAITextEmbeddingGeneration(Settings.AoaiEmbeddingDeploymentName, Settings.AoaiEndpoint, Settings.AoaiApiKey)
           
           .Build();
        

        kernel.ImportPluginFromType<EmployeePlugin>();
        kernel.ImportPluginFromType<TimePlugin>();
        kernel.ImportPluginFromPromptDirectory(@"SemanticPlugins\MailPlugin", "Email_Plugin");
        chat = kernel.Services.GetRequiredService<IChatCompletionService>();
    }
    static async Task Main(string[] args)
    {
        //semantic function to draft an email using employee list
        // kernel functions for employee management
        // TimePlugin from core plugins.

        OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
            MaxTokens=1000,
            Temperature=0.1
            
        };

        ChatHistory ch = new ChatHistory("you are an intelligent system to help user queries. Be very friendly and funny.");
        while (true)
        {
            WriteLine("------------------");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Write("User > ");
            var user_input = Console.ReadLine();
            ch.AddUserMessage(user_input);

            var results = chat.GetStreamingChatMessageContentsAsync(ch,openAIPromptExecutionSettings,kernel);

            string fullMessage = "";
            var first = true;
            Console.ForegroundColor = ConsoleColor.Green;
            await foreach (var content in results)
            {
                if (content.Role.HasValue && first)
                {
                    Console.Write("Assistant > ");
                    first = false;
                }
                Console.Write(content.Content);
                fullMessage += content.Content;
            }
            Console.WriteLine();

            // Add the message from the agent to the chat history
            ch.AddAssistantMessage(fullMessage);


        }

    }
}




