﻿using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.DtoModels
{
    public class ConfimationMailRequest
    {
        [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Not a valid email"), MaxLength(128, ErrorMessage = "Max 128 characters.")]
        public string Email { get; set; }
    }
}
