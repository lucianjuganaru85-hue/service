using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using AutoService.Data;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Runtime.InteropServices;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURARE CALENDAR ROMÂNĂ
var cultureInfo = new CultureInfo("ro-RO");
cultureInfo.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
cultureInfo.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// 2. CONFIGURARE BAZĂ DE DATE (Azure & Local)
string dbPath;
if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME") != null)
{
    dbPath = @"C:\home\CroxService.db";
}
else
{
    dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CroxService.db");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// 3. ADĂUGARE COMPONENTE RAZOR (Noul mod .NET 10)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// 4. CREARE AUTOMATĂ BAZĂ DE DATE
using (var scope = app.Services.CreateScope())
{
    try {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();
    } catch (Exception ex) {
        Console.WriteLine($"[DB ERROR] {ex.Message}");
    }
}

app.UseRequestLocalization(new RequestLocalizationOptions {
    DefaultRequestCulture = new RequestCulture(cultureInfo),
    SupportedCultures = new[] { cultureInfo },
    SupportedUICultures = new[] { cultureInfo }
});

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery(); // Necesar pentru Blazor Web App

app.MapRazorComponents<AutoService.Pages.App>() // Spunem că ușa e App.razor din folderul Pages
    .AddInteractiveServerRenderMode();

app.Run();