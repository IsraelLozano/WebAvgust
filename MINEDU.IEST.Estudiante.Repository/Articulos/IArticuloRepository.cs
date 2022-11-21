using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.Articulos
{
    public interface IArticuloRepository : IGenericRepository<Articulo>
    {
        Task<Articulo> GetArticuloFullById(int id);
    }
}
