using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shirzad.Core.ViewModels
{
    public class SliderViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please give it a name")]
        public string SliderTitle { get; set; }
        [Required(ErrorMessage = "Please give it an alt")]
        public string sliderAlt { get; set; }
        [Required(ErrorMessage = "Please give it an order")]
        public int SliderSort { get; set; }
        [Required(ErrorMessage = "Please choose an image")]
        public string SliderImg { get; set; }
        public bool IsActive { get; set; }
    }
}
