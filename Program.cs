using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Service.Data;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURARE CALENDAR ROMÂNĂ (Luni, zz.ll.aaaa)
var cultureInfo = new CultureInfo("ro-RO");
cultureInfo.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
cultureInfo.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// 2. CONFIGURARE BAZĂ DE DATE (Cale sigură pentru Azure)
// Pe Azure, folderul AppData este cel mai sigur pentru scriere
string dbPath;
if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME") != null)
{
    // Suntem pe Azure Windows
    dbPath = Path.Combine(@"C:\home\LogFiles", "CroxService.db");
}
else
{
    // Suntem local pe calculatorul tău
    dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CroxService.db");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// 3. INIȚIALIZARE BAZĂ DE DATE (Fără să omorâm aplicația dacă eșuează)
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        // Dacă eșuează, aplicația tot pornește, dar vom vedea eroarea în Log Stream
        Console.WriteLine($"[DB ERROR] Nu s-a putut crea baza de date: {ex.Message}");
    }
}

// 4. ACTIVARE LOCALIZARE
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