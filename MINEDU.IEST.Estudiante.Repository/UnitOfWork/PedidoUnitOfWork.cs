using IDCL.AVGUST.SIP.Contexto.DataPedido;
using IDCL.AVGUST.SIP.Repository.Pedido;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.UnitOfWork
{
    public class PedidoUnitOfWork : IUnitOfWork<DbPedidoContext>
    {
        private bool disposedValue;
        private DbPedidoContext _context;
        public IPedidoRepository _pedidoRepository { get; }
        public IPersonaRepository _personaRepository { get; }
        public ITipoCambioRepository _tipoCambioRepository{ get; }

        public PedidoUnitOfWork(DbPedidoContext context,
            IPedidoRepository pedidoRepository,
            IPersonaRepository personaRepository,
            ITipoCambioRepository tipoCambioRepository)
        {
            _context = context;
            _pedidoRepository = pedidoRepository;
            _personaRepository = personaRepository;
            _tipoCambioRepository = tipoCambioRepository;
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
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~PedidoUnitOfWork()
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
