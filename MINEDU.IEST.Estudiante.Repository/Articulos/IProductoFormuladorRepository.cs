using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.Articulos
{
    public interface IProductoFormuladorRepository : IGenericRepository<ProductoFormulador>
    {
        Task AddRangeProductoFormulador(List<ProductoFormulador> lista);
        Task DeleteForIdProducto(int idArticulo);
    }
}