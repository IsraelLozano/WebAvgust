using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Contexto;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.Articulos
{
    public class ProductoFabricanteRepository : GenericRepository<ProductoFabricante>, IProductoFabricanteRepository
    {
        private readonly dbContextAvgust _context;

        public ProductoFabricanteRepository(dbContextAvgust context) : base(context)
        {
            this._context = context;
        }

        public async Task DeleteForIdProducto(int idArticulo)
        {
            var lista = _context.ProductoFabricantes.Where(p => p.IdArticulo == idArticulo).ToList();
            _context.ProductoFabricantes.RemoveRange(lista);

        }


        public async Task AddRangeProductoFabricante(List<ProductoFabricante> lista)
        {
            await _context.ProductoFabricantes.AddRangeAsync(lista);

        }
    }
}
