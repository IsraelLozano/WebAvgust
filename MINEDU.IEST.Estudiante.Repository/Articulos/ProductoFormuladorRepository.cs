using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Contexto;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using Microsoft.EntityFrameworkCore;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.Articulos
{
    public class ProductoFormuladorRepository : GenericRepository<ProductoFormulador>, IProductoFormuladorRepository
    {
        private readonly dbContextAvgust _context;

        public ProductoFormuladorRepository(dbContextAvgust context) : base(context)
        {
            this._context = context;
        }

        public async Task DeleteForIdProducto(int idArticulo)
        {
            var lista = _context.ProductoFormuladors.Where(p => p.IdProducto == idArticulo).ToList();
            _context.ProductoFormuladors.RemoveRange(lista);
        }

        public async Task AddRangeProductoFormulador(List<ProductoFormulador> lista)
        {
            await _context.ProductoFormuladors.AddRangeAsync(lista);

        }


        /*
          public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

         */
    }
}
