﻿using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Contexto;
using IDCL.AVGUST.SIP.Repository.Maestra;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.UnitOfWork
{
    public class MaestraUnitOfWork : IUnitOfWork<dbContextAvgust>
    {
        private bool disposedValue;
        private readonly dbContextAvgust _context;
        public IPaisRepository _paisRepository { get; }
        public IFormuladorRepository _formuladorRepository { get; }
        public ITipoProductoRepository _tipoProductoRepository { get; }
        public ITitularRepository _titularRepository { get; }


        //Nuevos

        public ITipoDocumentoRepository _tipoDocumentoRepository { get; }
        public ICientificoPlagaRepository _cientificoPlagaRepository { get; }
        public ICultivoRepository _cultivoRepository { get; }
        public IAplicacionRepository _aplicacionRepository { get; }
        public IClaseRepository _claseRepository { get; }
        public IToxicologicaRepository _toxicologicaRepository { get; }
        public IGrupoQuimicoRepository _grupoQuimicoRepository { get; }
        public MaestraUnitOfWork(dbContextAvgust context, IPaisRepository paisRepository, IFormuladorRepository formuladorRepository, ITipoProductoRepository tipoProductoRepository, ITitularRepository titularRepository, ITipoDocumentoRepository tipoDocumentoRepository, ICientificoPlagaRepository cientificoPlagaRepository, ICultivoRepository cultivoRepository, IAplicacionRepository aplicacionRepository, IClaseRepository claseRepository, IToxicologicaRepository toxicologicaRepository, IGrupoQuimicoRepository grupoQuimicoRepository)
        {
            this._context = context;
            _paisRepository = paisRepository;
            _formuladorRepository = formuladorRepository;
            _tipoProductoRepository = tipoProductoRepository;
            _titularRepository = titularRepository;
            _tipoDocumentoRepository = tipoDocumentoRepository;
            _cientificoPlagaRepository = cientificoPlagaRepository;
            _cultivoRepository = cultivoRepository;
            _aplicacionRepository = aplicacionRepository;
            _claseRepository = claseRepository;
            _toxicologicaRepository = toxicologicaRepository;
            _grupoQuimicoRepository = grupoQuimicoRepository;
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
        // ~MaestraUnitOfWork()
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
