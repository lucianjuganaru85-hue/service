using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoService.Models
{
    public class ServiceItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AppointmentId { get; set; }

        [ForeignKey("AppointmentId")]
        public Appointment? Appointment { get; set; }

        // Legătura cu Magazia (nullable pentru că manopera nu are produs)
        public int? ProductId { get; set; }
        
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public double Quantity { get; set; } = 1;

        public decimal UnitPrice { get; set; }

        public double EstimatedHours { get; set; }

        [NotMapped]
        public decimal TotalPrice => (decimal)Quantity * UnitPrice;
    }
}