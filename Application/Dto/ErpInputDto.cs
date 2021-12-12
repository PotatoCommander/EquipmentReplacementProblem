﻿using System.Collections.Generic;

namespace DealerServiceSystem.Business.Dto
{
    public class ErpInputDto
    {
        public Dictionary<int, EquipmentServiceInformation> EquipmentServiceInformation { get; set; }

        public int YearStart { get; set; }

        public int EquipmentAgeAtStart { get; set; }
    }
}
