using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoService.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }
        
        [ForeignKey("CarId")]
        public Car? Car { get; set; }

        public int? AssignedMechanicId { get; set; }

        [ForeignKey("AssignedMechanicId")]
        public Employee? AssignedMechanic { get; set; }

        [Required]
        public DateTime ScheduledDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Programat";

        public string? Description { get; set; }

        // --- FUNCȚII NOI ADAUGATE ---
        public string? ClientIssues { get; set; }
        public string? MechanicFindings { get; set; }
        public bool IsDeleted { get; set; } = false;

        public List<ServiceItem> ServiceItems { get; set; } = new();
        
        public DateTime CreatedAt { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
    }
}