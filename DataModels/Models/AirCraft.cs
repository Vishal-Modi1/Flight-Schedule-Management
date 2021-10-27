using System;

namespace DataModels.Models
{
    public class AirCraft
    {
        public int Id { get; set; }
        public string TailNo { get; set; }
        public string ImageName { get; set; }
        public string Year { get; set; }
        public int AircraftMakeId { get; set; }
        public int AircraftModelId { get; set; }
        public int AircraftCategoryId { get; set; }
        public int AircraftClassId { get; set; }
        public int NoofEngines { get; set; }
        public bool IsEngineshavePropellers { get; set; }
        public bool IsEnginesareTurbines { get; set; }
        public bool TrackOilandFuel { get; set; }
        public bool IsIdentifyMeterMismatch { get; set; }
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
