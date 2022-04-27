using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shirzad.Core.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Enter the product name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter the product price")]
        public double Price { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime CreateDate { get; set; }

        public string AltProp { get; set; }
        [Required(ErrorMessage = "Enter the product details")]
        public string PrDescription { get; set; }

        public bool IsNew { get; set; }
        public bool IsActive { get; set; } 

        public ICollection<GalleryViewModel> Photos { get; set; }
        public int CategoryId { get; set; }
    }
}
