using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Service.Data;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Runtime.InteropServices;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURARE CALENDAR ROMÂNĂ (Luni, zz.ll.aaaa)
var cultureInfo = new CultureInfo("ro-RO");
cultureInfo.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
cultureInfo.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// DB PATH (Azure & Local)
string dbPath;
if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME") != null)
{
    dbPath = Path.Combine(@"C:\home\LogFiles", "CroxService.db");
}
else
{
    dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CroxService.db");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// AUTO-CREATE DATABASE
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[DB ERROR] {ex.Message}");
    }
}

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(cultureInfo),
    SupportedCultures = new[] { cultureInfo },
    SupportedUICultures = new[] { cultureInfo }
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();