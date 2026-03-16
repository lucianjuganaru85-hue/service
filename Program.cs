using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Service.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurarea Bazei de Date (SQLite)
// Folosim o cale care să funcționeze și pe Windows-ul tău și pe serverul Azure
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=CroxService.db";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

// 2. Adăugarea serviciilor pentru Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// 3. LOGICA DE ACTIVARE A BAZEI DE DATE (Critic pentru Azure)
// Acest bloc verifică la pornire dacă baza de date există și o creează dacă lipsește
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        // EnsureCreated se asigură că fișierul .db și tabelele sunt făcute instant
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "A apărut o eroare la crearea bazei de date.");
    }
}

// 4. Configurarea conductei HTTP
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