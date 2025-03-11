using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<WikipediaService>();

// ��������� WikipediaService � ���������
builder.Services.AddSingleton<WikipediaService>();

// ��������� ����������
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// ��������� ������ �� ���������� � ��������� ����������
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

var app = builder.Build();  // ��� ����������� ��'��� app

// ������������ ����������
var supportedCultures = new[] { new CultureInfo("uk-UA"), new CultureInfo("en-US") };
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("uk-UA"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};
app.UseRequestLocalization(localizationOptions);

// ������������ ������� HTTP ������
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

app.Run();  // ������ �������
