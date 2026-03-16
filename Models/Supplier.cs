using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoService.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string CUI { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<SupplierInvoice> Invoices { get; set; } = new();
    }
}