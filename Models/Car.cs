using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoService.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Numărul de înmatriculare este obligatoriu")]
        public string LicensePlate { get; set; } = string.Empty;

        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? VIN { get; set; }
        
        // Câmpuri numerice (Asigură-te că ai rulat scriptul SQL "Final Boss")
        public int Year { get; set; }
        public string? EngineCode { get; set; }
        public string? FuelType { get; set; }
        public int HorsePower { get; set; }
        public int EngineCapacity { get; set; }
        public int CurrentMileage { get; set; }
        
        public string? Observations { get; set; }

        [Required]
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client? Client { get; set; }

        public bool IsDeleted { get; set; } = false;

        // Aceasta este legătura care dădea eroarea CS0246
        public List<Appointment> Appointments { get; set; } = new();
    }
}