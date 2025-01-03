﻿using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.Models
{
    public class TemplateDetails
    {
        [Key]
        public int TemplateDetailsId { get; set; }
        public int? TemplateId { get; set; }
        [Required]
        public int AccountId { get; set; }
        [Required]
        public bool IsDebit{ get; set; }

        public virtual Template Template { get; set; }
        public virtual Account Account { get; set; }
    }
}
