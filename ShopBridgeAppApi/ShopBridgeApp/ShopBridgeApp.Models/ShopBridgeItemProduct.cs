using System;
using System.ComponentModel.DataAnnotations;

namespace ShopBridgeApp.Models
{
    public class ShopBridgeItemProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
