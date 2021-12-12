using EquipmentReplacementProblem.Business.Core.Algorithms;
using EquipmentReplacementProblem.Business.Dto;
using EquipmentReplacementProblem.Business.Services.Interfaces;

namespace EquipmentReplacementProblem.Business.Services
{
    public class ErpBellmanFordSolutionService : IErpSolutionService
    {
        public ErpOutputDto FindOptimalStrategy(ErpInputDto erpInputDto)
        {
            var erpSolver = new ErpBellmanFord(erpInputDto);
            erpSolver.PrintShortestPath();
            return erpSolver.GetSolutionData();
        }
    }
}
