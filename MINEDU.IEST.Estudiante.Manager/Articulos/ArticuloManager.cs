using AutoMapper;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using IDCL.AVGUST.SIP.ManagerDto.Articulos;
using IDCL.AVGUST.SIP.ManagerDto.Articulos.Add;
using IDCL.AVGUST.SIP.ManagerDto.Maestros;
using IDCL.AVGUST.SIP.Repository.UnitOfWork;

namespace IDCL.AVGUST.SIP.Manager.Articulos
{
    public class ArticuloManager : IArticuloManager
    {
        private readonly IMapper _mapper;
        private readonly ArticuloUnitOfWork _articuloUnitOfWork;
        private readonly MaestraUnitOfWork _maestraUnitOfWork;

        public ArticuloManager(IMapper mapper, ArticuloUnitOfWork _articuloUnitOfWork, MaestraUnitOfWork maestraUnitOfWork)
        {
            this._mapper = mapper;
            this._articuloUnitOfWork = _articuloUnitOfWork;
            this._maestraUnitOfWork = maestraUnitOfWork;
        }

        public async Task<List<GetArticuloDto>> GetListArticulos()
        {
            var query = _articuloUnitOfWork._articuloRepository.GetAll(includeProperties: "IdFormuladorNavigation,IdPaisNavigation,IdTipoProductoNavigation,IdTitularRegistroNavigation", orderBy: p => p.OrderByDescending(l => l.IdArticulo));
            var response = _mapper.Map<List<GetArticuloDto>>(query);
            return response;
        }

        public async Task<GetArticuloForEditDto> GetArticuloById(int id)
        {
            var query = await _articuloUnitOfWork._articuloRepository.GetArticuloFullById(id);
            var response = new GetArticuloForEditDto { articulo = new GetArticuloDto() };
            if (query != null)
            {
                response.articulo = _mapper.Map<GetArticuloDto>(query);

                response.articulo.Composicions = _mapper.Map<List<GetComposicionDto>>(query.Composicions);
                response.articulo.Documentos = _mapper.Map<List<GetDocumentoDto>>(query.Documentos);
                response.articulo.Usos = _mapper.Map<List<GetUsoDto>>(query.Usos);
                response.articulo.Caracteristicas = _mapper.Map<List<GetCaracteristicaDto>>(query.Caracteristicas);
            }
            response.paises = _mapper.Map<List<GetPaisDto>>(_maestraUnitOfWork._paisRepository.GetAll());
            response.formuladores = _mapper.Map<List<GetFormuladorDto>>(_maestraUnitOfWork._formuladorRepository.GetAll());
            response.tiposProductos = _mapper.Map<List<GetIdTipoProductoDto>>(_maestraUnitOfWork._tipoProductoRepository.GetAll());
            response.titulares = _mapper.Map<List<GetTitularRegistroDto>>(_maestraUnitOfWork._titularRepository.GetAll());
            response.tiposDocumentos = _mapper.Map<List<GetTipoDocumentoDto>>(_maestraUnitOfWork._tipoDocumentoRepository.GetAll());
            response.tiposPlagas = _mapper.Map<List<GetCientificoPlagaDto>>(_maestraUnitOfWork._cientificoPlagaRepository.GetAll());
            response.tiposCultivos = _mapper.Map<List<GetCultivoDto>>(_maestraUnitOfWork._cultivoRepository.GetAll());
            response.cboAplicaciones = _mapper.Map<List<GetAplicacionDto>>(_maestraUnitOfWork._aplicacionRepository.GetAll());
            response.cboClase = _mapper.Map<List<GetClaseDto>>(_maestraUnitOfWork._claseRepository.GetAll());
            response.cboToxicologica = _mapper.Map<List<GetToxicologicaDto>>(_maestraUnitOfWork._toxicologicaRepository.GetAll());
            response.cboGrupoQuimico = _mapper.Map<List<GetGrupoQuimicoDto>>(_maestraUnitOfWork._grupoQuimicoRepository.GetAll());

            return response;
        }

        public async Task<AddOrEditArticuloDto> CreateOrUpdateArticulo(AddOrEditArticuloDto model)
        {

            var articulo = _mapper.Map<Articulo>(model);
            if (model.IdArticulo == 0)
            {
                _articuloUnitOfWork._articuloRepository.Insert(articulo);
            }
            else
            {
                _articuloUnitOfWork._articuloRepository.Update(articulo);
            }

            await _articuloUnitOfWork.SaveAsync();

            model = _mapper.Map<AddOrEditArticuloDto>(articulo);
            return model;
        }

        #region Composicion

        public async Task<AddOrEditComposicionDto> CreateOrUpdateComposicion(AddOrEditComposicionDto model)
        {

            var composicion = _mapper.Map<Composicion>(model);
            if (model.IdArticulo == 0)
            {
                var item = _articuloUnitOfWork._composicionRepository.GetAll(p => p.IdArticulo == model.IdArticulo).Max(p => p.Iditem) + 1;
                composicion.Iditem = item;
                _articuloUnitOfWork._composicionRepository.Insert(composicion);
            }
            else
            {
                _articuloUnitOfWork._composicionRepository.Update(composicion);
            }

            await _articuloUnitOfWork.SaveAsync();

            model = _mapper.Map<AddOrEditComposicionDto>(composicion);
            return model;
        }

        public async Task<List<GetComposicionDto>> GetListComposicionByIdArticulo(int idArticulo)
        {
            try
            {
                var query = _articuloUnitOfWork._composicionRepository.GetAll(p => p.IdArticulo == idArticulo);
                return _mapper.Map<List<GetComposicionDto>>(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Caracteristicas

        public async Task<AddOrEditCaracteristicaDto> CreateOrUpdateCaracteristica(AddOrEditCaracteristicaDto model)
        {

            var caracteristica = _mapper.Map<Caracteristica>(model);
            if (model.IdArticulo == 0)
            {
                var item = _articuloUnitOfWork._caracteristicaRepository.GetAll(p => p.IdArticulo == model.IdArticulo).Max(p => p.IdItem) + 1;
                caracteristica.IdItem = item;
                _articuloUnitOfWork._caracteristicaRepository.Insert(caracteristica);
            }
            else
            {
                _articuloUnitOfWork._caracteristicaRepository.Update(caracteristica);
            }

            await _articuloUnitOfWork.SaveAsync();

            model = _mapper.Map<AddOrEditCaracteristicaDto>(caracteristica);
            return model;
        }

        public async Task<List<GetCaracteristicaDto>> GetListCaracteristicaByIdArticulo(int idArticulo)
        {
            try
            {
                var query = _articuloUnitOfWork._caracteristicaRepository.GetAll(p => p.IdArticulo == idArticulo,includeProperties: "IdAplicacionNavigation,IdClaseNavigation,IdToxicologicaNavigation") ;
                return _mapper.Map<List<GetCaracteristicaDto>>(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Caracteristicas

        public async Task<AddOrEditDocumentoDto> CreateOrUpdateDocumento(AddOrEditDocumentoDto model)
        {

            var documento = _mapper.Map<Documento>(model);
            if (model.IdArticulo == 0)
            {
                var item = _articuloUnitOfWork._documentoRepository.GetAll(p => p.IdArticulo == model.IdArticulo).Max(p => p.IdItem) + 1;
                documento.IdItem = item;
                _articuloUnitOfWork._documentoRepository.Insert(documento);
            }
            else
            {
                _articuloUnitOfWork._documentoRepository.Update(documento);
            }

            await _articuloUnitOfWork.SaveAsync();

            model = _mapper.Map<AddOrEditDocumentoDto>(documento);
            return model;
        }

        #endregion

        #region Caracteristicas

        public async Task<AddOrEditUsoDto> CreateOrUpdateUso(AddOrEditUsoDto model)
        {

            var uso = _mapper.Map<Uso>(model);
            if (model.IdArticulo == 0)
            {
                var item = _articuloUnitOfWork._usoRepository.GetAll(p => p.IdArticulo == model.IdArticulo).Max(p => p.IdItem) + 1;
                uso.IdItem = item;

                _articuloUnitOfWork._usoRepository.Insert(uso);
            }
            else
            {
                _articuloUnitOfWork._usoRepository.Update(uso);
            }

            await _articuloUnitOfWork.SaveAsync();

            model = _mapper.Map<AddOrEditUsoDto>(uso);
            return model;
        }

        #endregion


    }
}
