using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<WikipediaService>();

// Реєстрація WikipediaService в контейнері
builder.Services.AddSingleton<WikipediaService>();

// Додавання локалізації
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Додавання сервісів до контейнера з підтримкою локалізації
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

var app = builder.Build();  // Тут створюється об'єкт app

// Налаштування локалізації
var supportedCultures = new[] { new CultureInfo("uk-UA"), new CultureInfo("en-US") };
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("uk-UA"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};
app.UseRequestLocalization(localizationOptions);

// Налаштування конвеєра HTTP запитів
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();  // Запуск додатку
