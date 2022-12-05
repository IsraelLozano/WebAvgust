using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Contexto;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using Microsoft.EntityFrameworkCore;
using MINEDU.IEST.Estudiante.Inf_Utils.Helpers;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.Seguridad
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        private readonly dbContextAvgust _context;

        public UsuarioRepository(dbContextAvgust context) : base(context)
        {
            this._context = context;
        }


        public async Task<bool> UpdatePassword(string codigo, string newClave)
        {
            var query = await _context.Usuarios.FirstOrDefaultAsync(p => p.Credencial == codigo);

            query.Clave = EncryptHelper.EncryptToByte(newClave);
            this.Update(query);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
