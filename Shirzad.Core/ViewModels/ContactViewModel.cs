using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shirzad.Core.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your Phone number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please enter your Email address")]
        [EmailAddress(ErrorMessage ="Please enter a valid email address")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please enter your Message")]
        public string Message { get; set; }
    }
}
