using IDCL.AVGUST.SIP.Entity.Calculator;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.Calculator
{
    public interface IArticuloCalcRepository : IGenericRepository<ArticuloServ>
    {
        Task<List<ArticuloServ>> GetArticulosAll(string filter);
    }
}
