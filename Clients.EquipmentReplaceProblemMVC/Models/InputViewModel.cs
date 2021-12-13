using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentReplacementProblem.Business.Dto;

namespace Clients.EquipmentReplaceProblemMVC.Models
{
    public class InputViewModel
    {
        public InputViewModel()
        {
            Infos = new List<EquipmentServiceInformation>();
        }

        public List<EquipmentServiceInformation> Infos { get; set; }

        public int EquipmentAgeAtStart { get; set; }

        public int StartYear { get; set; }

        public int YearsCount { get; set; }
    }
}
