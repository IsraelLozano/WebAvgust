using IDCL.AVGUST.SIP.ManagerDto.Calculator.ArticuloCalc;

namespace IDCL.AVGUST.SIP.Manager.Calculator
{
    public interface IArticuloCalculatorManager
    {
        Task<GetArticuloAllDto> GetArticuloCals(string filter);
    }
}