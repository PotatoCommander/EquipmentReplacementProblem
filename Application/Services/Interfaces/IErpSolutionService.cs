using EquipmentReplacementProblem.Business.Dto;

namespace EquipmentReplacementProblem.Business.Services.Interfaces
{
    public interface IErpSolutionService
    {
        public ErpOutputDto FindOptimalStrategy(ErpInputDto erpInputDto);
    }
}
