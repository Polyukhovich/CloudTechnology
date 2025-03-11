using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Azure;
using Azure.AI.TextAnalytics;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // API-���� � �������
        private static readonly string languageKey = "FsCOX0mTwl8C4FXOcCK2hxV90efi0BoXKBRnV6vzgptM2t9dNefyJQQJ99BCACYeBjFXJ3w3AAAaACOGp1Wy";  // API ����
        private static readonly string languageEndpoint = "https://lab4sevices.cognitiveservices.azure.com/";  //  �������

        // ����������� �볺���
        private readonly TextAnalyticsClient _client = new TextAnalyticsClient(
            new Uri(languageEndpoint), new AzureKeyCredential(languageKey));

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // ³���������� ��������� �������
        public IActionResult Index()
        {
            return View(new EntityModel());
        }

        // ĳ� ��� ������ ��������� ������
        [HttpPost]
        public IActionResult AnalyzeText(string inputText)
        {
            var model = new EntityModel { InputText = inputText, Entities = new List<EntityResult>() };

            if (!string.IsNullOrEmpty(inputText))
            {
                // ��������� ������������ ���������
                var response = _client.RecognizeLinkedEntities(inputText);

                foreach (var entity in response.Value)
                {
                    // ���������� �� � �������� � Matches
                    if (entity.Matches.Any())  // �������� �� �������� ���� � ������ �����
                    {
                        var firstMatch = entity.Matches.First();  // �������� ������ ����
                        model.Entities.Add(new EntityResult
                        {
                            Name = entity.Name,
                            Url = entity.Url.ToString(),  // ������������ Url � �����
                            ConfidenceScore = firstMatch.ConfidenceScore
                        });
                    }
                }
            }

            return View("Index", model);
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
