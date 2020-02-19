using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username not specified")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password don't match")]
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required(ErrorMessage = "Email not specified")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect email")]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
