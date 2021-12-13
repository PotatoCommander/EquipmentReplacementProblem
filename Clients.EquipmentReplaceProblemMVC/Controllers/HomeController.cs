using Microsoft.AspNetCore.Mvc;
using Clients.EquipmentReplaceProblemMVC.Models;

namespace Clients.EquipmentReplaceProblemMVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new InputSettingsViewModel()
            {
                EquipmentAgeAtStart = 1,
                StartYear = 1,
                YearsCount = 5
            });
        }

        [HttpPost]
        public IActionResult Index(InputSettingsViewModel model)
        {
            return RedirectToAction("Calculate", "Calculation", model);
        }
    }
}
