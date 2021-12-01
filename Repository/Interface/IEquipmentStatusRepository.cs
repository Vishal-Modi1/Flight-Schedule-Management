using DataModels.Models;
using System.Collections.Generic;

namespace Repository.Interface
{
    public interface IEquipmentStatusRepository
    {
        List<EquipmentStatus> List();
    }
}
