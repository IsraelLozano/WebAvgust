using IDCL.Tacama.Core.Contexto.IDCL.Tacama.Core.Contexto;
using IDCL.Tacama.Core.Entity;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.Tacama
{
    public class TramaDiarioRepository : GenericRepository<TramaDiario>, ITramaDiarioRepository
    {
        public TramaDiarioRepository(DbTacamaContext context) : base(context)
        {

        }
    }
}
