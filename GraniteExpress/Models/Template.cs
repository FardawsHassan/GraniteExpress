﻿using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.Models
{
    public class Template
    {
        [Key]
        public int TemplateId { get; set; }
        [Required]
        public string TemplateName { get; set; }
        [Required]
        public bool isActive { get; set; } = false;

        public virtual List<TemplateDetails> TemplateDetail { get; set; }
        public virtual List<Journal> Journals { get; set; }
    }
}
