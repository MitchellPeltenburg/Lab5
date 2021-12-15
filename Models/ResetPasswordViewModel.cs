using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class ResetPasswordViewModel
    {
        //Email address with 'Required' decorator so Identity Framework knows it must have a value
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //Password with 'Required' decorator so Identity Framework knows it must have a value
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //ConfirmPassword with 'Required' decorator so Identity Framework knows it must have a value, as well as a compare to Password so Identity Framework knows it must be the same as Password
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }
        
        //Randomly generated password reset token
        public string Token { get; set; }
    }
}
