using DataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.VM
{
    public class AirCraftVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tail No")]
        public string TailNo { get; set; }

        [Display(Name = "Image")]
        public string ImageName { get; set; }
        public string Year { get; set; }

        [Required]
        [Display(Name = "Make")]
        public int AircraftMakeId { get; set; }

        [Required]
        [Display(Name = "Model")]
        public int AircraftModelId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int AircraftCategoryId { get; set; }

        [Display(Name = "Class")]
        public int AircraftClassId { get; set; }

        [Display(Name = "No of Engines")]
        public int NoofEngines { get; set; }

        [Display(Name = "Engines have Propellers")]
        public bool IsEngineshavePropellers { get; set; }

        [Display(Name = "Engines are Turbines")]
        public bool IsEnginesareTurbines { get; set; }

        [Display(Name = "Track Oil and Fuel")]
        public bool TrackOilandFuel { get; set; }

        [Display(Name = "Identify Meter Mismatch")]
        public bool IsIdentifyMeterMismatch { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> UpdatedBy { get; set; }


        //Dropdowns list

        public List<AircraftMake> AircraftMakeList { get; set; }
        public List<AircraftModel> AircraftModelList { get; set; }
        public List<AircraftCategory> AircraftCategoryList { get; set; }
        public List<AircraftClass> AircraftClassList { get; set; }

    }
}
