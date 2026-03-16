using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoService.Models
{
    public class SupplierInvoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier? Supplier { get; set; }

        [Required]
        public string InvoiceNumber { get; set; } = string.Empty;

        [Required]
        public DateTime InvoiceDate { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

        public DateTime DueDate { get; set; } = DateTime.SpecifyKind(DateTime.Now.AddDays(14), DateTimeKind.Utc);

        public decimal TotalValue { get; set; }

        public bool IsPaid { get; set; } = false;

        public bool IsDeleted { get; set; } = false;

        // Aceasta este linia care genera eroarea dacă SupplierInvoiceItem nu era găsit
        public List<SupplierInvoiceItem> SupplierInvoiceItems { get; set; } = new();
    }
}