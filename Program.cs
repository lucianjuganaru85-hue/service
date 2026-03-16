using AutoService.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurare Bază de Date (SQLite pentru portabilitate online)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Servicii esențiale
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Activare Blazor Interactive Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Servicii custom (Email, etc.)
builder.Services.AddScoped<AutoService.Services.EmailService>();

var app = builder.Build();

// 4. Configurare Limba Română (Format zz.ll.aaaa și luni ca primă zi)
var supportedCultures = new[] { new CultureInfo("ro-RO") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("ro-RO"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// Configurare mediu
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthorization();

app.MapControllers();

// Mapare componente Blazor (Asigură-te că namespace-ul corespunde proiectului tău)
app.MapRazorComponents<Service.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();