using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoService.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Prenumele este obligatoriu")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Numele este obligatoriu")]
        public string LastName { get; set; } = string.Empty;

        public string? Email { get; set; }

        [Required(ErrorMessage = "Telefonul este obligatoriu")]
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }
        
        // Câmpuri necesare pentru QuickReception
        public string? City { get; set; }
        public string? County { get; set; }

        // Date Firmă
        public bool IsCompany { get; set; } = false;
        public string? CUI { get; set; }
        public string? RegCom { get; set; }

        // Soft Delete
        public bool IsDeleted { get; set; } = false;

        // Navigare necesară pentru ClientsController
        public List<Car> Cars { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
    }
}