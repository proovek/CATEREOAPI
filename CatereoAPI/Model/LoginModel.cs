﻿using System.ComponentModel.DataAnnotations;

namespace CatereoAPI.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        public string? Password { get; set; }

    }
}
