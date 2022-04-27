using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shirzad.Core.ViewModels
{
    public class GalleryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please choose a file")]
        public string Url { get; set; }
        public int ProductId { get; set; }
    }
}
