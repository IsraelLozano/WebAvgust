using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Contexto;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using Microsoft.EntityFrameworkCore;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.Seguridad
{
    public class UsuarioPaisRepository : GenericRepository<UsuarioPai>, IUsuarioPaisRepository
    {
        private readonly dbContextAvgust _context;

        public UsuarioPaisRepository(dbContextAvgust context) : base(context)
        {
            this._context = context;
        }

        //public Task<List<UsuarioPai>> GetListUsuarioPaisByIdUsuario(int idUsuario)
        //{
        //    var query = _context.Pais.ToListAsync();
        //    var up = _context.UsuarioPais.Where(p => p.IdUsuario == idUsuario).ToListAsync();
            

        //}
    }
}
