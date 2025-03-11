using System;
using Azure;
using Azure.AI.TextAnalytics;

namespace EntityLinkingExample
{
    class Program
    {
        // Встав свій API-ключ та ендпоінт із порталу Azure
        static string languageKey = "FsCOX0mTwl8C4FXOcCK2hxV90efi0BoXKBRnV6vzgptM2t9dNefyJQQJ99BCACYeBjFXJ3w3AAAaACOGp1Wy";  // 🔹  ключ API
        static string languageEndpoint = "https://lab4sevices.cognitiveservices.azure.com/";  // 🔹 ендпоінт

        private static readonly AzureKeyCredential credentials = new AzureKeyCredential(languageKey);
        private static readonly Uri endpoint = new Uri(languageEndpoint);

        // Метод для розпізнавання зв’язаних сутностей у тексті
        static void EntityLinkingExample(TextAnalyticsClient client)
        {
            string text = "Microsoft was founded by Bill Gates and Paul Allen on April 4, 1975, " +
                          "to develop and sell BASIC interpreters for the Altair 8800. " +
                          "During his career at Microsoft, Gates held the positions of chairman, " +
                          "chief executive officer, president, and chief software architect, " +
                          "while also being the largest individual shareholder until May 2014.";

            Response<LinkedEntityCollection> response = client.RecognizeLinkedEntities(text);


            Console.WriteLine("Linked Entities:");
            foreach (var entity in response.Value)
            {
                Console.WriteLine($"\tName: {entity.Name},\tID: {entity.DataSourceEntityId},\tURL: {entity.Url}\tData Source: {entity.DataSource}");

                Console.WriteLine("\tMatches:");
                foreach (var match in entity.Matches)
                {
                    Console.WriteLine($"\t\tText: {match.Text}");
                    Console.WriteLine($"\t\tScore: {match.ConfidenceScore:F2}\n");
                }
            }
        }

        static void Main(string[] args)
        {
            var client = new TextAnalyticsClient(endpoint, credentials);
            EntityLinkingExample(client);

            Console.Write("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
