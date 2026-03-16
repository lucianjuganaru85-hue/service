using System;

namespace AutoService.Models
{
    public class Part
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // ex: Set plăcuțe frână, Ulei 5W30
        public string Code { get; set; } = string.Empty; // Cod intern sau cod producător (OEM)
        public string Manufacturer { get; set; } = string.Empty; // ex: Bosch, Castrol, OE VW
        
        // Detalii stoc și alerte
        public int StockQuantity { get; set; } = 0;
        public int MinimumStockAlert { get; set; } = 5; // Când stocul scade sub 5, sistemul cere reaprovizionare
        
        // Zona financiară
        public decimal PurchasePrice { get; set; } // Prețul de achiziție de la furnizor (fără TVA)
        public decimal SellingPrice { get; set; } // Prețul de vânzare către client (fără TVA)
        
        // Gestiune depozit
        public string StorageLocation { get; set; } = string.Empty; // ex: Raft A, Rândul 3
    }
}