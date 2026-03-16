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

        // Tabelele tale oficiale, exact cum le cer paginile tale Razor
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