using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shirzad.Core.ViewModels
{
    public class WebPayOnlineViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter Web Pay Title")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter Web Pay Description")]
        public string Description { get; set; }
      
        public string PhotoUrl { get; set; }

        public string ImageLink { get; set; }
    }
}
