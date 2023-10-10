using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend_Task.Models.Product
{
    public class BaseProduct
    {
        [Key]
        public string? ProductCode { get; set; }
        public string? Category { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public int MinQuantity { get; set; }
        public double DiscountRate { get; set; }

        public IFormFile File { get; set; }
    }
}
