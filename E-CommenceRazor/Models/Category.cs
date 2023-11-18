﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace E_CommenceRazor.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must between 1 and 100")]
        public int DisplayOrder { get; set; }
    }
}
