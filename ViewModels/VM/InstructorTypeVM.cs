﻿using System.ComponentModel.DataAnnotations;

namespace ViewModels.VM
{
    public class InstructorTypeVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The instructor type field is required")]
        [Display(Name = "Instructor Type")]
        public string Name { get; set; }

        public int TotalRecords { get; set; }

    }
}
