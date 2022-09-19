using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.VM
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
