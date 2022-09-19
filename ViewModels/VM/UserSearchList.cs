namespace ViewModels.VM
{
    public class UserSearchList
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsInstructor { get; set; }
        
        public bool IsActive { get; set; }

        public string UserRole { get; set; }

        public int TotalRecords { get; set; }
    }
}
