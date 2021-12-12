using DealerServiceSystem.Business.Dto;

namespace DealerServiceSystem.Business.Services.Interfaces
{
    public interface IErpSolutionService
    {
        public ErpOutputDto FindOptimalStrategy(ErpInputDto erpInputDto);
    }
}
