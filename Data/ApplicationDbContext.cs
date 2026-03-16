using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AutoService.Models;

namespace Service.Data;
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }ausing Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
// Dacă eroarea persistă la linia de mai jos, verifică numele proiectului tău!
using Service.Data; 
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

// 2. CONFIGURARE BAZĂ DE DATE (Cale sigură pentru Azure Windows)
string dbPath;
if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME") != null)
{
    // Calea pentru Azure Windows - în folderul de LogFiles avem drept de scriere
    dbPath = Path.Combine(@"C:\home\LogFiles", "CroxService.db");
}
else
{
    // Calea pentru calculatorul tău (Desktop)
    dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CroxService.db");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// 3. INIȚIALIZARE BAZĂ DE DATE (Creare automată la pornire)
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[DB ERROR] {ex.Message}");
    }
}

// 4. ACTIVARE LOCALIZARE (Calendarul în română)
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

        public DbSet<Client> Clients { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierInvoice> SupplierInvoices { get; set; }
        public DbSet<StandardService> StandardServices { get; set; }
        
        // Tabelul pentru produsele din facturile de achiziție
        public DbSet<SupplierInvoiceItem> SupplierInvoiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}