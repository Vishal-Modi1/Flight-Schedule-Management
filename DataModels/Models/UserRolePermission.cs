namespace DataModels.Models
{
    public class UserRolePermission
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public int ModuleId { get; set; }
        public bool IsAllowed { get; set; }
    }
}
