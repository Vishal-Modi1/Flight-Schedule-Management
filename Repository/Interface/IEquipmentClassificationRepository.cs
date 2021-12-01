using DataModels.Models;
using System.Collections.Generic;

namespace Repository.Interface
{
    public interface IEquipmentClassificationRepository
    {
        List<EquipmentClassification> List();
    }
}
