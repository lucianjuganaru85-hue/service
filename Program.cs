using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using AutoService.Data;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Runtime.InteropServices;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURARE CALENDAR ROMÂNĂ (Luni, zz.ll.aaaa)
var cultureInfo = new CultureInfo("ro-RO");
cultureInfo.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
cultureInfo.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// 2. CALEA BAZEI DE DATE (Azure & Local)
string dbPath;
if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME") != null)
{
    // Pe Azure scriem într-un loc cu permisiuni garantate
    dbPath = @"C:\home\CroxService.db";
}
else
{
    // Pe calculatorul tău (Desktop)
    dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CroxService.db");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// 3. CREARE AUTOMATĂ BAZĂ DE DATE
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
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();