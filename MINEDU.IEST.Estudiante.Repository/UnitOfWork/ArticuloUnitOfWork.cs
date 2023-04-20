using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Contexto;
using IDCL.AVGUST.SIP.Repository.Articulos;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.UnitOfWork
{
    public class ArticuloUnitOfWork : IUnitOfWork<dbContextAvgust>
    {
        private bool disposedValue;
        private readonly dbContextAvgust _context;
        public IArticuloRepository _articuloRepository { get; }
        public ICaracteristicaRepository _caracteristicaRepository { get; }
        public IComposicionRepository _composicionRepository { get; }
        public IDocumentoRepository _documentoRepository { get; }
        public IUsoRepository _usoRepository { get; }

        public IProductoFormuladorRepository _productoFormuladorRepository { get; }
        public IProductoFabricanteRepository _productoFabricanteRepository { get; }

        public ArticuloUnitOfWork(dbContextAvgust context,
            IArticuloRepository articuloRepository,
            ICaracteristicaRepository caracteristicaRepository,
            IComposicionRepository composicionRepository,
            IDocumentoRepository documentoRepository,
            IUsoRepository usoRepository,
            IProductoFormuladorRepository productoFormuladorRepository,
            IProductoFabricanteRepository productoFabricanteRepository)
        {
            this._context = context;
            _articuloRepository = articuloRepository;
            _caracteristicaRepository = caracteristicaRepository;
            _composicionRepository = composicionRepository;
            _documentoRepository = documentoRepository;
            _usoRepository = usoRepository;
            _productoFormuladorRepository = productoFormuladorRepository;
            _productoFabricanteRepository = productoFabricanteRepository;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ArticuloUnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
