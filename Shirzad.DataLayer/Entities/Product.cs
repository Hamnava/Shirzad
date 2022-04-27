
using System.ComponentModel.DataAnnotations;

namespace Shirzad.DataLayer.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public string AltProp { get; set; }
        public string PrDescription { get; set; }
        public bool IsNew { get; set; }
        public bool BestSaller { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        public string PhotoUrl { get; set; }
        public ICollection<ProductGallery> ProductGalleries { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
