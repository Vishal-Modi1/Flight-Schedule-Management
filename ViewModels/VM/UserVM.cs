﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.VM
{
    public class UserVM
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = "Is Send Email Invite")]
        public Nullable<bool> IsSendEmailInvite { get; set; }

        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Phone { get; set; }

        [Display(Name = "User Role")]
        public int RoleId { get; set; }

        [Display(Name = "Is Instructor")]
        public Nullable<bool> IsInstructor { get; set; }

        [Display(Name = "Instructor Type")]
        public Nullable<int> InstructorTypeId { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "External Id")]
        public string ExternalId { get; set; }

        [Display(Name = "Date of Birth")]
        public Nullable<System.DateTime> DateofBirth { get; set; }
        public string Gender { get; set; }

        [Display(Name = "Country")]
        public Nullable<int> CountryId { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> UpdatedBy { get; set; }

        public List<UserRoleVM> UserRoles { get; set; }
        public List<InstructorTypeVM> InstructorTypes { get; set; }
        public List<CountryVM> Countries { get; set; }
    }
}
