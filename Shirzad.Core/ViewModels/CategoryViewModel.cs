

using System.ComponentModel.DataAnnotations;

namespace Shirzad.Core.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter the Category Name😂")]
        public string Name { get; set; }
        public bool IsDelete { get; set; }

       
    }
}
