using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class RegisterViewModel
    {
        //Email address with 'Required' decorator so Identity Framework knows it must have a value
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //Password with 'Required' decorator so Identity Framework knows it must have a value, as well as a StringLength decorator so it must be within a certain number of characters
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //ConfirmPassword with 'Required' decorator so Identity Framework knows it must have a value, as well as a compare to Password so Identity Framework knows it must be the same as Password
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
