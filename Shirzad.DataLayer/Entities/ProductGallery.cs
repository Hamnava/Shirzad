using System.ComponentModel.DataAnnotations;

namespace Shirzad.DataLayer.Entities
{
    public class ProductGallery
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }


        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
