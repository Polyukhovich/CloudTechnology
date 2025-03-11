using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using Azure;
using Azure.AI.TextAnalytics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks; // ��������� ��� ������������ ���������

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // API-���� � ������� (Azure Text Analytics)
        private static readonly string languageKey = "FsCOX0mTwl8C4FXOcCK2hxV90efi0BoXKBRnV6vzgptM2t9dNefyJQQJ99BCACYeBjFXJ3w3AAAaACOGp1Wy";
        private static readonly string languageEndpoint = "https://lab4sevices.cognitiveservices.azure.com/";

        private readonly TextAnalyticsClient _client = new TextAnalyticsClient(
            new Uri(languageEndpoint), new AzureKeyCredential(languageKey));

        private readonly WikipediaService _wikipediaService; // ������ ��� �����䳿 � ³���䳺�

        public HomeController(ILogger<HomeController> logger, WikipediaService wikipediaService)
        {
            _logger = logger;
            _wikipediaService = wikipediaService; // ����������� ������
        }

        public IActionResult Index()
        {
            return View(new EntityModel());
        }

        // ����� ����������� ������
        [HttpPost]
        public async Task<IActionResult> AnalyzeText(string inputText) // ������� �����������
        {
            var model = new EntityModel { InputText = inputText, Entities = new List<EntityResult>() };

            if (!string.IsNullOrEmpty(inputText))
            {
                var response = _client.RecognizeLinkedEntities(inputText);

                foreach (var entity in response.Value)
                {
                    if (entity.Matches.Any())
                    {
                        var firstMatch = entity.Matches.First();
                        var entityResult = new EntityResult
                        {
                            Name = entity.Name,
                            Url = entity.Url.ToString(),
                            ConfidenceScore = firstMatch.ConfidenceScore
                        };

                        // �������� ����� � ³���䳿
                        entityResult.Description = await _wikipediaService.GetWikipediaSummaryAsync(entity.Url.ToString());

                        model.Entities.Add(entityResult); // ������ ����� ��������� � ������
                    }
                }
            }

            // �������� ������ ����� ������
            model.InputText = GetFirstParagraph(inputText);

            return View("Index", model);
        }

        // ����� ��� �������� ������� ������
        private string GetFirstParagraph(string text)
        {
            var paragraphs = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            return paragraphs.Length > 0 ? paragraphs[0] : text;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
