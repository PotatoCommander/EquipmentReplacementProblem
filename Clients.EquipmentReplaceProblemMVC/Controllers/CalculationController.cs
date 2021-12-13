using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Clients.EquipmentReplaceProblemMVC.Models;
using EquipmentReplacementProblem.Business.Dto;
using EquipmentReplacementProblem.Business.Services.Interfaces;
using EquipmentReplacementProblem.Render.Drawing;
using EquipmentReplacementProblem.Render.Helpers;
using Microsoft.Extensions.Logging;

namespace Clients.EquipmentReplaceProblemMVC.Controllers
{
    public class CalculationController : Controller
    {
        private readonly ILogger<CalculationController> _logger;
        private readonly IErpSolutionService _erpSolutionService;

        public CalculationController(ILogger<CalculationController> logger, IErpSolutionService erpSolutionService)
        {
            _logger = logger;
            _erpSolutionService = erpSolutionService;
        }

        [HttpGet]
        public IActionResult Calculate(InputSettingsViewModel inputSettings)
        {
            return View("~/Views/Calculation/Input.cshtml", new InputViewModel()
            {
                EquipmentAgeAtStart = inputSettings.EquipmentAgeAtStart,
                StartYear = inputSettings.StartYear,
                YearsCount = inputSettings.YearsCount,
                Infos = (new List<EquipmentServiceInformation>(new EquipmentServiceInformation[inputSettings.YearsCount]))
                    .Select(x => new EquipmentServiceInformation()).ToList(),
            });
        }

        [HttpPost]
        public IActionResult Calculate(InputViewModel inputModel)
        {
            var dictionary = new Dictionary<int, EquipmentServiceInformation>();
            for (var i = 0; i < inputModel.Infos.Count; i++)
            {
                dictionary.Add(i, inputModel.Infos[i]);
            }

            var graphInfo = new ErpInputDto()
            {
                EquipmentAgeAtStart = inputModel.EquipmentAgeAtStart,
                EquipmentServiceInformation = dictionary,
                YearStart = inputModel.StartYear
            };

            var solution = _erpSolutionService.FindOptimalStrategy(graphInfo);
            var converted = ErpOutputToGraphRenderModelConverter.ConvertOutputToRenderModel(solution);
            var renderer = new GraphRender();
            var bmp = renderer.DrawGraph(converted);
            return View("~/Views/Calculation/Solve.cshtml", new ErpProblemIndexViewModel()
            {
                GraphImage = BitmapToBytes(bmp),
                Path = solution.OptimalPath.Select(x => x.Name).Aggregate("", (current, next) => current + "\n" + next) 
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
