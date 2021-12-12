using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EquipmentReplacementProblem.Business.Dto;
using EquipmentReplacementProblem.Business.Services;
using EquipmentReplacementProblem.Render.Drawing;
using EquipmentReplacementProblem.Render.Helpers;

namespace EquipmentReplacementProblem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ErpProblemController : ControllerBase
    {
        [HttpPost("solve")]
        public async Task<ActionResult> SolveErp(ErpInputDto model)
        {
            var solver = new ErpBellmanFordSolutionService();
            var result = solver.FindOptimalStrategy(model);
            var visualModel = ErpOutputToGraphRenderModelConverter.ConvertOutputToRenderModel(result);
            var a = new GraphRender();
            var bmp = a.DrawGraph(visualModel);
            return Ok(visualModel);
        }
    }
}
