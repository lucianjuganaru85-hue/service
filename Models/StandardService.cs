using System.ComponentModel.DataAnnotations;

namespace AutoService.Models
{
    public class StandardService
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        public decimal DefaultPrice { get; set; }
        
        public double DefaultHours { get; set; }
    }
}