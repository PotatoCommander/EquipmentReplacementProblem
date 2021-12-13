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
    public class InputController : Controller
    {
        private readonly ILogger<InputController> _logger;
        private readonly IErpSolutionService _erpSolutionService;

        public InputController(ILogger<InputController> logger, IErpSolutionService erpSolutionService)
        {
            _logger = logger;
            _erpSolutionService = erpSolutionService;
        }

        [HttpGet]
        public IActionResult Input(InputSettingsViewModel inputSettings)
        {
            return View(new InputViewModel()
            {
                EquipmentAgeAtStart = inputSettings.EquipmentAgeAtStart,
                StartYear = inputSettings.StartYear,
                YearsCount = inputSettings.YearsCount,
                Infos = (new List<EquipmentServiceInformation>(new EquipmentServiceInformation[inputSettings.YearsCount]))
                    .Select(x => new EquipmentServiceInformation()).ToList(),
            });
        }

        [HttpPost]
        public IActionResult Input(InputViewModel inputModel)
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
            return View("~/Views/ErpProblem/Solve.cshtml", new ErpProblemIndexViewModel()
            {
                GraphImage = BitmapToBytes(bmp),
                Path = solution.OptimalPath.Select(x => x.Name).Aggregate("", (current, next) => current + "----> " + next) 
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
