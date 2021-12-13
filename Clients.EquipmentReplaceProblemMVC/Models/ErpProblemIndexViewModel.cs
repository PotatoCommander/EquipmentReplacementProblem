using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Clients.EquipmentReplaceProblemMVC.Models
{
    public class ErpProblemIndexViewModel
    {
        public byte[] GraphImage { get; set; }

        public string Path { get; set; }
    }
}
