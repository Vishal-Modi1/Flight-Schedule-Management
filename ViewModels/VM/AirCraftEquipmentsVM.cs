using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.VM
{
    public class AirCraftEquipmentsVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Required]
        [Display(Name = "Item")]
        public string Item { get; set; }

        [Required]
        [Display(Name = "Classification")]
        public string ClassificationId { get; set; }

        
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Make")]
        public string Make { get; set; }

        [Required]
        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }

        [Required]
        [Display(Name = "Part Number")]
        public string PartNumber { get; set; }

        [Required]
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }
        [Required]
        [Display(Name = "Manufacturer Date")]
        public Nullable<DateTime> ManufacturerDate { get; set; } 
        
        
        [Required]
        [Display(Name = "Aircraft TT at Install")]
        public Nullable<int> AircraftTTInstall { get; set; }

        [Required]
        [Display(Name = "Part TT at Install")]
        public Nullable<int> PartTTInstall { get; set; }

        [Required]
        [Display(Name = "Log Entry Date")]
        public Nullable<DateTime> LogEntryDate { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; } 
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
 
    }
}
