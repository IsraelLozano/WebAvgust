using IDCL.Tacama.Core.Entity;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.Tacama
{
    public interface IUsuarioTacRepository : IGenericRepository<UsuarioTacama>
    {
        Task<UsuarioTacama> Autenticar(string usuario, byte[] clave);
        Task<UsuarioTacama> GetCredencials(int idPersona);
    }
}