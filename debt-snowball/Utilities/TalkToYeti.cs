using Azure;
using Azure.AI.OpenAI;
using System.Text;

namespace debt_snowball.Utilities
{
    public class TalkToYeti
{
        public async Task<string> CallYeti(double amount, double months)
        {
            string proxyUrl = "https://aoai.hacktogether.net";
            string key = "YOUR_API_KEY";

            // the full url is appended by /v1/api
            Uri proxyUri = new(proxyUrl + "/v1/api");

            // the full key is appended by "/YOUR-GITHUB-ALIAS"
            AzureKeyCredential token = new(key + "/yohuck");

            // instantiate the client with the "full" values for the url and key/token
            OpenAIClient openAIClient = new(proxyUri, token);

            ChatCompletionsOptions completionOptions = new()
            {
                MaxTokens = 500,
                Temperature = 0.7f,
                NucleusSamplingFactor = 0.95f,
                DeploymentName = "gpt-3.5-turbo"
            };

            completionOptions.Messages.Add(new ChatMessage(ChatRole.System, "RAWR! You are a goofy cartoon Yeti who speaks like a Snowboarder bro, hates debt, and should say something quick but encouraging including reiterating how much they are saving over how many months. Maybe make some monster sounds."));
            completionOptions.Messages.Add(new ChatMessage(ChatRole.User, $"I will pay off my debt {months} months early and save ${amount}"));

            var response = await openAIClient.GetChatCompletionsAsync(completionOptions);

            var str = response.Value.Choices[0].Message.Content.ToString();

            return str;
        }
}
}
