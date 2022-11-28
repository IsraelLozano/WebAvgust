using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Contexto;
using IDCL.AVGUST.SIP.Repository.Seguridad;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.UnitOfWork
{
    public class SeguridadUnitOfWork : IUnitOfWork<dbContextAvgust>
    {
        private bool disposedValue;
        public IUsuarioRepository _usuarioRepositoy { get; }
        public IUsuarioPaisRepository _usuarioPaisRepository { get; }
        private dbContextAvgust _context;

        public SeguridadUnitOfWork(IUsuarioRepository usuarioRepositoy, dbContextAvgust context, IUsuarioPaisRepository usuarioPaisRepository)
        {
            _usuarioRepositoy = usuarioRepositoy;
            _context = context;
            _usuarioPaisRepository = usuarioPaisRepository;
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
        // ~SeguridadUnitOfWork()
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
