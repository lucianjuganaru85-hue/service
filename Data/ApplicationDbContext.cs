using Microsoft.EntityFrameworkCore;
using AutoService.Models;

namespace AutoService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Punem toate tabelele pe care aplicația ta le caută (numele în engleză conform erorilor)
        public DbSet<Client> Clients { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierInvoice> SupplierInvoices { get; set; }
        public DbSet<SupplierInvoiceItem> SupplierInvoiceItems { get; set; }
        public DbSet<StandardService> StandardServices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }
    }
}

// Această punte este necesară pentru ca Program.cs să funcționeze corect
namespace Service.Data
{
    public class ApplicationDbContext : AutoService.Data.ApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<AutoService.Data.ApplicationDbContext> options) : base(options) { }
    }
}