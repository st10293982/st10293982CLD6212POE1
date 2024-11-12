﻿using System.ComponentModel.DataAnnotations;

namespace st10293982CLD6212POE1.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]

        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }    
    }
}
