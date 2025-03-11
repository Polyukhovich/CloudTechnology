using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class WikipediaService
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<string> GetWikipediaSummaryAsync(string url)
    {
        // URL для API Вікіпедії (ми отримуємо перший абзац тексту сторінки)
        var pageTitle = GetPageTitleFromUrl(url);
        var apiUrl = $"https://en.wikipedia.org/api/rest_v1/page/summary/{pageTitle}";

        try
        {
            var response = await client.GetStringAsync(apiUrl);
            // Тут можна обробити JSON відповідь, щоб отримати необхідний абзац.
            var summary = ParseWikipediaSummary(response);
            return summary;
        }
        catch (Exception)
        {
            return "Не вдалося отримати інформацію.";
        }
    }

    private string GetPageTitleFromUrl(string url)
    {
        // Витягнення заголовка сторінки з URL (це просто приклад для Вікіпедії)
        var uri = new Uri(url);
        var title = uri.AbsolutePath.Replace("/wiki/", "");
        return title;
    }

    private string ParseWikipediaSummary(string jsonResponse)
    {
        // Для простоти ми припускаємо, що JSON містить поле 'extract' з першим абзацом
        var json = JsonSerializer.Deserialize<JsonElement>(jsonResponse);
        return json.GetProperty("extract").GetString();
    }
}
