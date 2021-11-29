using System.Collections.Generic;

namespace ViewModels.VM.UserRolePermission
{
    public class Menus
    {
        List<MenuItem> MenuItems { get; set; }
    }
    public class MenuItem
    {
        public string Action { get; set; }
        public string DisplayName { get; set; }
        public string Controller { get; set; }
        public string FavIconStyle { get; set; }
        public List<MenuItem> MenuItems { get; set; }
    }
}
