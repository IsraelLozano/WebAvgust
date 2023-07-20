using IDCL.AVGUST.SIP.Contextos.IDCL.AVGUST.SIP.Contexto;
using IDCL.AVGUST.SIP.Repository.Calculator;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.UnitOfWork
{
    public class CalculatorUnitOfWork : IUnitOfWork<DbAvgustCalcContext>
    {
        private bool disposedValue;

        private readonly DbAvgustCalcContext _context;
        public IArticuloCalcRepository _articuloCalcRepository { get; }
        public IArticuloCategoriaRepository _articuloCategoriaRepository { get; }
        public ITasaComisionRepository _tasaComisionRepository { get; }
        public ISimuladorPedidoRepository _simuladorPedidoRepository { get; }
        public ISimuladorPedidoDetalleRepository _simuladorPedidoDetalleRepository { get; }

        public CalculatorUnitOfWork(DbAvgustCalcContext context,
            IArticuloCalcRepository articuloCalcRepository,
            IArticuloCategoriaRepository articuloCategoriaRepository,
            ISimuladorPedidoRepository simuladorPedidoRepository,
            ISimuladorPedidoDetalleRepository simuladorPedidoDetalleRepository,
            ITasaComisionRepository tasaComisionRepository)
        {
            _context = context;
            _articuloCalcRepository = articuloCalcRepository;
            _articuloCategoriaRepository = articuloCategoriaRepository;
            _simuladorPedidoRepository = simuladorPedidoRepository;
            _simuladorPedidoDetalleRepository = simuladorPedidoDetalleRepository;
            _tasaComisionRepository = tasaComisionRepository;
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
                    this._context.Dispose();
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~CalculatorUnitOfWork()
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
