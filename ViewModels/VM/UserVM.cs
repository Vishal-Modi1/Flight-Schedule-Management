

using System;
using System.Collections.Generic;

namespace ViewModels.VM
{
    public class UserVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<bool> IsSendEmailInvite { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public Nullable<bool> IsIntructor { get; set; }
        public Nullable<int> InstructorTypeId { get; set; }
        public string CompanyName { get; set; }
        public string ExternalId { get; set; }
        public Nullable<System.DateTime> DateofBirth { get; set; }
        public string Gender { get; set; }
        public Nullable<int> CountryId { get; set; }
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
