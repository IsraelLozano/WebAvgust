using IDCL.Tacama.Core.Contexto.IDCL.Tacama.Core.Contexto;
using IDCL.Tacama.Core.Entity;
using Microsoft.EntityFrameworkCore;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.Tacama
{
    public class UsuarioTacRepository : GenericRepository<UsuarioTacama>, IUsuarioTacRepository
    {
        private readonly DbTacamaContext _context;

        public UsuarioTacRepository(DbTacamaContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<UsuarioTacama> Autenticar(string usuario, byte[] clave)
        {
            var query = await _context.Usuarios
                .Where(l => l.Credencial.ToUpper() == usuario.ToUpper() && l.Clave == clave)
                .Select(s => new UsuarioTacama
                {
                    IdPersona = s.IdPersona,
                    NombreCorto = s.Credencial,
                    UsuarioRols = s.UsuarioRols.Select(r => new UsuarioRol
                    {
                        IdRolNavigation = new Rol
                        {
                            IdRol = r.IdRol,
                            Nombre = r.IdRolNavigation.Nombre
                        }
                    }).ToList()
                }).FirstOrDefaultAsync();

            return query;
        }

        public async Task<UsuarioTacama> GetCredencials(int id) => await _context.Usuarios.FirstOrDefaultAsync(l => l.IdPersona == id);

    }
}
