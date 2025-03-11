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

        // API-ключ і ендпоінт
        private static readonly string languageKey = "FsCOX0mTwl8C4FXOcCK2hxV90efi0BoXKBRnV6vzgptM2t9dNefyJQQJ99BCACYeBjFXJ3w3AAAaACOGp1Wy";  // API ключ
        private static readonly string languageEndpoint = "https://lab4sevices.cognitiveservices.azure.com/";  //  ендпоінт

        // Ініціалізація клієнта
        private readonly TextAnalyticsClient _client = new TextAnalyticsClient(
            new Uri(languageEndpoint), new AzureKeyCredential(languageKey));

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Відображення початкової сторінки
        public IActionResult Index()
        {
            return View(new EntityModel());
        }

        // Дія для аналізу введеного тексту
        [HttpPost]
        public IActionResult AnalyzeText(string inputText)
        {
            var model = new EntityModel { InputText = inputText, Entities = new List<EntityResult>() };

            if (!string.IsNullOrEmpty(inputText))
            {
                // Виконання розпізнавання сутностей
                var response = _client.RecognizeLinkedEntities(inputText);

                foreach (var entity in response.Value)
                {
                    // Перевіряємо чи є елементи у Matches
                    if (entity.Matches.Any())  // Перевірка на наявність хоча б одного матчу
                    {
                        var firstMatch = entity.Matches.First();  // Отримуємо перший матч
                        model.Entities.Add(new EntityResult
                        {
                            Name = entity.Name,
                            Url = entity.Url.ToString(),  // Перетворюємо Url у рядок
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
