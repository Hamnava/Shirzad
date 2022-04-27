using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shirzad.Core.ViewModels
{
    public class PricingViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your Name")]
        public string Name { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter your Phone Number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please enter your Email Address")]
        [EmailAddress(ErrorMessage ="Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your Message for order")]
        public string Message { get; set; }
    }
}
