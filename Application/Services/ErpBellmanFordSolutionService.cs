using DealerServiceSystem.Business.Core.Algorithms;
using DealerServiceSystem.Business.Dto;
using DealerServiceSystem.Business.Services.Interfaces;

namespace DealerServiceSystem.Business.Services
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
