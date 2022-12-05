using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.Seguridad
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<bool> UpdatePassword(string codigo, string newClave);
    }
}
