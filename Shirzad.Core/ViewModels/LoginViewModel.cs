using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shirzad.Core.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your Usename")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter your Password")]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool Ispersistant { get; set; }
    }
}
