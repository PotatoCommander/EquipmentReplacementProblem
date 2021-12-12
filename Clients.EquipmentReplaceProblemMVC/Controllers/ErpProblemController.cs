using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Clients.EquipmentReplaceProblemMVC.Models;
using EquipmentReplacementProblem.Business.Dto;
using EquipmentReplacementProblem.Business.Services.Interfaces;
using EquipmentReplacementProblem.Render.Drawing;
using EquipmentReplacementProblem.Render.Helpers;

namespace Clients.EquipmentReplaceProblemMVC.Controllers
{
    public class ErpProblemController : Controller
    {
        private readonly ILogger<ErpProblemController> _logger;
        private readonly IErpSolutionService _erpSolutionService;

        public ErpProblemController(ILogger<ErpProblemController> logger, IErpSolutionService erpSolutionService)
        {
            _logger = logger;
            _erpSolutionService = erpSolutionService;
        }

        public IActionResult Index()
        {
            var info = new ErpInputDto()
            {
                EquipmentAgeAtStart = 3,
                YearStart = 1,
                EquipmentServiceInformation = new Dictionary<int, EquipmentServiceInformation>()
                {
                    {
                        0, new EquipmentServiceInformation()
                        {
                            Income = 20000,
                            ServiceExpenses = 200,
                            SellEquipmentCost = 100000,
                            NewEquipmentCost = 100000
                        }
                    },
                    {
                        1, new EquipmentServiceInformation()
                        {
                            Income = 19000,
                            ServiceExpenses = 600,
                            SellEquipmentCost = 80000,
                            NewEquipmentCost = 100000
                        }
                    },
                    {
                        2, new EquipmentServiceInformation()
                        {
                            Income = 18500,
                            ServiceExpenses = 1200,
                            SellEquipmentCost = 60000,
                            NewEquipmentCost = 100000
                        }
                    },
                    {
                        3, new EquipmentServiceInformation()
                        {
                            Income = 17200,
                            ServiceExpenses = 1500,
                            SellEquipmentCost = 50000,
                            NewEquipmentCost = 100000
                        }
                    },
                    {
                        4, new EquipmentServiceInformation()
                        {
                            Income = 15500,
                            ServiceExpenses = 1700,
                            SellEquipmentCost = 30000,
                            NewEquipmentCost = 100000
                        }
                    },
                    {
                        5, new EquipmentServiceInformation()
                        {
                            Income = 14000,
                            ServiceExpenses = 1800,
                            SellEquipmentCost = 10000,
                            NewEquipmentCost = 100000
                        }
                    },
                    {
                        6, new EquipmentServiceInformation()
                        {
                            Income = 12200,
                            ServiceExpenses = 2200,
                            SellEquipmentCost = 5000,
                            NewEquipmentCost = 100000
                        }
                    }
                }
            };
            var solution = _erpSolutionService.FindOptimalStrategy(info);
            var converted = ErpOutputToGraphRenderModelConverter.ConvertOutputToRenderModel(solution);
            var renderer = new GraphRender();
            var bmp = renderer.DrawGraph(converted);
            return View(new ErpProblemIndexViewModel()
            {
                GraphImage = BitmapToBytes(bmp)
            });
        }

        private static byte[] BitmapToBytes(Bitmap img)
        {
            using var stream = new MemoryStream();
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
    }
}
