using IDCL.AVGUST.SIP.Contextos.IDCL.AVGUST.SIP.Contexto;
using IDCL.AVGUST.SIP.Entity.Calculator;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.Calculator
{
    public class TasaComisionRepository : GenericRepository<TasasComision>, ITasaComisionRepository
    {
        public TasaComisionRepository(DbAvgustCalcContext context) : base(context)
        {
        }
    }
}
