using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.Articulos
{
    public interface IProductoFabricanteRepository : IGenericRepository<ProductoFabricante>
    {
        Task AddRangeProductoFabricante(List<ProductoFabricante> lista);
        Task DeleteForIdProducto(int idArticulo);
    }
}