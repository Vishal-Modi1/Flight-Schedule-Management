namespace DataModels.Models
{
    public class UserRolePermission
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string ModuleName { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanView { get; set; }
        public bool CanDelete { get; set; }
    }
}
