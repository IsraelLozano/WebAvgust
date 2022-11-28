using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Contexto;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using Microsoft.EntityFrameworkCore;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.Maestra
{
    public class PaisRepository : GenericRepository<Pai>, IPaisRepository
    {
        private readonly dbContextAvgust _context;

        public PaisRepository(dbContextAvgust context) : base(context)
        {
            this._context = context;
        }


        public async Task<List<Pai>> GetListUsuarioPaisByIdUsuario(int idUsuario)
        {
            var query = await _context.Pais
                .Select(p => new Pai
                {
                    IdPais = p.IdPais,
                    NomPais = p.NomPais,
                    UsuarioPais = p.UsuarioPais.Where(u => u.IdUsuario == idUsuario).ToList()
                })
                .ToListAsync();

            return query;
        }

    }
}
