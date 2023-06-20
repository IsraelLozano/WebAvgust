using IDCL.AVGUST.SIP.ManagerDto.Calculator.ArticuloCalc;

namespace IDCL.AVGUST.SIP.Manager.Calculator
{
    public interface IArticuloCalculatorManager
    {
        Task<List<GetArticuloCalDto>> GetArticuloCals(string filter);
    }
}