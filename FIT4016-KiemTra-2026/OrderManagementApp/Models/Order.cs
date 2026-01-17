// Models/Order.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int ProductId { get; set; }
        
        [Required]
        [StringLength(20)]
        [RegularExpression(@"^ORD-\d{8}-\d{4}$")]
        public string OrderNumber { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;
        
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string CustomerEmail { get; set; } = string.Empty;
        
        [Required]
        public DateTime OrderDate { get; set; }
        
        public DateTime? DeliveryDate { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation property
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; } = null!;
        
        // Computed property
        [NotMapped]
        public string Status => DeliveryDate.HasValue ? "Delivered" : "Pending";
    }
}