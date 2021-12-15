using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class LoginViewModel
    {
        //Email address with 'Required' decorator so Identity Framework knows it must have a value
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //Password with 'Required' decorator so Identity Framework knows it must have a value
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //For the 'Remember Me?' checkbox on login page
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
