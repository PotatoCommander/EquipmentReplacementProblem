using System;
using System.Collections.Generic;
using System.Text;

namespace DealerServiceSystem.Business.Core.Models
{
    public class EquipmentUsageInformation
    {
        public Dictionary<int, AtomicEquipmentInfo> EquipmentInfo { get; set; }
    }
}
