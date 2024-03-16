﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulkyweb_razor.Models {
    public class Category {

        [Key]
        public int Id { get; set; }
        [Required, DisplayName("Category Name"), MaxLength(30)]
        public string CategoryName { get; set; }
        [DisplayName("Display Order"), Range(1, 100, ErrorMessage = "Enter value between 1-100")]
        public int DisplayOrder { get; set; }
    }
}
