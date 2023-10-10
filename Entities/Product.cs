using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Task.Entities
{
    public class Product
    {
        [Key]
        public string? ProductCode { get; set; }
        public string? Category { get; set; }
        public string? Name { get; set; }
        public string? FileUrl { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

        public double Price { get; set; }
        public int MinQuantity { get; set; }
        public double DiscountRate { get; set; }

    }
}
