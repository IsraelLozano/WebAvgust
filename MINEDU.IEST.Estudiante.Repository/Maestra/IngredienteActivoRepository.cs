using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Contexto;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.Maestra
{
    public class IngredienteActivoRepository : GenericRepository<IngredienteActivo>, IIngredienteActivoRepository
    {
        public IngredienteActivoRepository(dbContextAvgust context) : base(context)
        {

        }
    }
}
