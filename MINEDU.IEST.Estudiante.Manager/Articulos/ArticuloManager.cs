using AutoMapper;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using IDCL.AVGUST.SIP.ManagerDto.Articulos;
using IDCL.AVGUST.SIP.ManagerDto.Articulos.Add;
using IDCL.AVGUST.SIP.ManagerDto.Maestros;
using IDCL.AVGUST.SIP.Repository.UnitOfWork;
using MINEDU.IEST.Estudiante.Inf_Utils.Dtos;
using MINEDU.IEST.Estudiante.Inf_Utils.Enumerados;
using MINEDU.IEST.Estudiante.Inf_Utils.Helpers.FileManager;

namespace IDCL.AVGUST.SIP.Manager.Articulos
{
    public class ArticuloManager : IArticuloManager
    {
        private readonly IMapper _mapper;
        private readonly ArticuloUnitOfWork _articuloUnitOfWork;
        private readonly MaestraUnitOfWork _maestraUnitOfWork;
        private readonly ResourceDto _resourceDto;
        private readonly IStorageManager _storageManager;
        private readonly SeguridadUnitOfWork _seguridadUnitOfWork;
        public ArticuloManager(IMapper mapper, ArticuloUnitOfWork _articuloUnitOfWork, MaestraUnitOfWork maestraUnitOfWork, ResourceDto resourceDto, IStorageManager storageManager, SeguridadUnitOfWork seguridadUnitOfWork)
        {
            this._mapper = mapper;
            this._articuloUnitOfWork = _articuloUnitOfWork;
            this._maestraUnitOfWork = maestraUnitOfWork;
            this._resourceDto = resourceDto;
            _storageManager = storageManager;
            _seguridadUnitOfWork = seguridadUnitOfWork;
        }

        public async Task<List<GetArticuloDto>> GetListArticulos(int IdUsuario, int tipoFiltro, string filtro, int idIngredienteActivo)
        {
            var user = _seguridadUnitOfWork._usuarioRepositoy.GetAll(p => p.IdUsuario == IdUsuario, includeProperties: "UsuarioPais,UsuarioPais.IdPaisNavigation").FirstOrDefault();
            var paises = user.UsuarioPais.Select(p => p.IdPais).ToList();

            var filter = new List<Articulo>();

            if ((int)TipoBusquedaArticulo.nombre == tipoFiltro)
            {
                var query = _articuloUnitOfWork._articuloRepository
                    .GetAll(p => paises.Contains(p.IdPais.Value)
                    && p.FlgActivo,
                    includeProperties: "IdPaisNavigation,IdTipoProductoNavigation,IdTitularRegistroNavigation",
                    orderBy: p => p.OrderByDescending(l => l.IdArticulo)).AsEnumerable();

                filter = query.Where(p => filtro.Contains(p.NombreComercial, StringComparison.CurrentCultureIgnoreCase) || p.NombreComercial.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else if ((int)TipoBusquedaArticulo.ingredienteActivo == tipoFiltro)
            {
                var query = _articuloUnitOfWork._articuloRepository
                   .GetAll(p => paises.Contains(p.IdPais.Value)
                   && p.FlgActivo
                   && p.Composicions.Any(l => l.IngredienteActivo == idIngredienteActivo),
                   includeProperties: "IdPaisNavigation,IdTipoProductoNavigation,IdTitularRegistroNavigation",
                   orderBy: p => p.OrderByDescending(l => l.IdArticulo)).AsEnumerable();
                filter = query.ToList();
            }

            var response = _mapper.Map<List<GetArticuloDto>>(filter);
            return response;
        }

        public async Task<GetArticuloForEditDto> GetArticuloById(int id)
        {
            try
            {
                var query = await _articuloUnitOfWork._articuloRepository.GetArticuloFullById(id);
                var response = new GetArticuloForEditDto { articulo = new GetArticuloDto() };
                if (query != null)
                {
                    response.articulo = _mapper.Map<GetArticuloDto>(query);

                    response.articulo.Composicions = _mapper.Map<List<GetComposicionDto>>(query.Composicions);
                    response.articulo.Documentos = _mapper.Map<List<GetDocumentoDto>>(query.Documentos);
                    response.articulo.Usos = _mapper.Map<List<GetUsoDto>>(query.Usos);
                    response.articulo.ProductoFabricantes = _mapper.Map<List<GetProductoFabricanteDto>>(query.ProductoFabricantes);
                    response.articulo.ProductoFormuladors = _mapper.Map<List<GetProductoFormuladorDto>>(query.ProductoFormuladors);


                    var etiqueta = await this.GetEtiquetaDocumento(id);
                    response.articulo.Usos.ForEach(p =>
                    {
                        p.Dosis = etiqueta;
                    });

                    response.articulo.Caracteristicas = _mapper.Map<List<GetCaracteristicaDto>>(query.Caracteristicas);
                }

                response.paises = _mapper.Map<List<GetPaisDto>>(_maestraUnitOfWork._paisRepository.GetAll(p => p.estado, orderBy: p => p.OrderBy(l => l.NomPais)));
                response.formuladores = _mapper.Map<List<GetFormuladorDto>>(_maestraUnitOfWork._formuladorRepository.GetAll(p => p.estado, orderBy: p => p.OrderBy(l => l.NomFormulador)));
                response.tiposProductos = _mapper.Map<List<GetIdTipoProductoDto>>(_maestraUnitOfWork._tipoProductoRepository.GetAll(p => p.estado, orderBy: p => p.OrderBy(l => l.NomTipoProducto)));
                response.titulares = _mapper.Map<List<GetTitularRegistroDto>>(_maestraUnitOfWork._titularRepository.GetAll(p => p.estado, orderBy: p => p.OrderBy(l => l.NomTitularRegistro)));
                response.tiposDocumentos = _mapper.Map<List<GetTipoDocumentoDto>>(_maestraUnitOfWork._tipoDocumentoRepository.GetAll(p => p.estado, orderBy: p => p.OrderBy(l => l.Nombre)));
                response.tiposPlagas = _mapper.Map<List<GetCientificoPlagaDto>>(_maestraUnitOfWork._cientificoPlagaRepository.GetAll(p => p.estado, orderBy: p => p.OrderBy(l => l.NombreCientificoPlaga)));
                response.tiposCultivos = _mapper.Map<List<GetCultivoDto>>(_maestraUnitOfWork._cultivoRepository.GetAll(p => p.estado, orderBy: p => p.OrderBy(l => l.NombreCultivo)));
                response.cboAplicaciones = _mapper.Map<List<GetAplicacionDto>>(_maestraUnitOfWork._aplicacionRepository.GetAll(p => p.estado, orderBy: p => p.OrderBy(l => l.Descripcion)));
                response.cboClase = _mapper.Map<List<GetClaseDto>>(_maestraUnitOfWork._claseRepository.GetAll(p => p.estado, orderBy: p => p.OrderBy(l => l.Descripcion)));
                response.cboToxicologica = _mapper.Map<List<GetToxicologicaDto>>(_maestraUnitOfWork._toxicologicaRepository.GetAll(p => p.estado, orderBy: p => p.OrderBy(l => l.Descripcion)));
                response.cboGrupoQuimico = _mapper.Map<List<GetGrupoQuimicoDto>>(_maestraUnitOfWork._grupoQuimicoRepository.GetAll(p => p.estado, orderBy: p => p.OrderBy(l => l.NomGrupoQuimico)));
                response.cboTipoFormulacion = _mapper.Map<List<GetTipoFormulacionDto>>(_maestraUnitOfWork._tipoFormulacionRepository.GetAll(p => p.estado, orderBy: p => p.OrderBy(l => l.CodTipoFormulacion)));
                response.cboTipoIngredienteActivo = _mapper.Map<List<GetTipoIngredienteActivoDto>>(_maestraUnitOfWork._ingredienteActivoRepository.GetAll(p => p.estado, orderBy: p => p.OrderBy(l => l.NomIngredienteActivo)));
                response.cboFabricante = _mapper.Map<List<GetFabricanteDto>>(_maestraUnitOfWork._fabricanteRepository.GetAll(p => p.Estado, orderBy: p => p.OrderBy(l => l.NombreFabricante)));

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> DeleteArticuloById(int id)
        {
            try
            {
                var data = _articuloUnitOfWork._articuloRepository.GetById(id);
                data.FlgActivo = false;
                await _articuloUnitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<AddOrEditArticuloDto> CreateOrUpdateArticulo(AddOrEditArticuloDto model)
        {

            var articulo = _mapper.Map<Articulo>(model);
            articulo.FlgActivo = true;
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

        #region Visualizados PDF
        public async Task<GetPdfDto> GetArticuloDocumentoPdf(int idArticulo, int idItem)
        {
            var doc = _articuloUnitOfWork._documentoRepository.GetAll(p => p.IdArticulo == idArticulo && p.IdItem == idItem).FirstOrDefault();
            var ruta = Path.Combine(_resourceDto.UrlFileBase, _resourceDto.Documents, doc.NomDocumento);
            if (!System.IO.File.Exists(ruta))
                throw new FileNotFoundException($"Archivo no existe: {doc.NomDocumento}");

            MemoryStream _output = new MemoryStream(System.IO.File.ReadAllBytes(ruta));
            var pdf64 = _storageManager.GetBase64(_output);
            GetPdfDto data = new GetPdfDto
            {
                base64 = pdf64
            };
            return data;
        }

        #endregion

        #region Composicion

        public async Task<AddOrEditComposicionDto> CreateOrUpdateComposicion(AddOrEditComposicionDto model)
        {

            try
            {
                var composicion = _mapper.Map<Composicion>(model);
                if (model.Iditem == 0)
                {
                    var item = _articuloUnitOfWork._composicionRepository.GetAll(p => p.IdArticulo == model.IdArticulo);
                    composicion.Iditem = item.Count() == 0 ? 1 : item.Max(p => p.Iditem) + 1;
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
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<GetComposicionDto>> GetListComposicionByIdArticulo(int idArticulo)
        {

            try
            {
                var query = _articuloUnitOfWork._composicionRepository.GetAll(p => p.IdArticulo == idArticulo, includeProperties: "IngredienteActivoNavigation,GrupoQuimicoNavegation");
                return _mapper.Map<List<GetComposicionDto>>(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> DeleteComposicionByItem(int idArticulo, int item)
        {

            try
            {
                var data = _articuloUnitOfWork._composicionRepository.GetAll(p => p.IdArticulo == idArticulo && p.Iditem == item).FirstOrDefault();
                _articuloUnitOfWork._composicionRepository.Delete(data);

                await _articuloUnitOfWork.SaveAsync();
                return true;
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
            if (model.IdItem == 0)
            {
                var item = _articuloUnitOfWork._caracteristicaRepository.GetAll(p => p.IdArticulo == model.IdArticulo);
                caracteristica.IdItem = item.Count() == 0 ? 1 : item.Max(p => p.IdItem) + 1;
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
                var query = _articuloUnitOfWork._caracteristicaRepository.GetAll(p => p.IdArticulo == idArticulo, includeProperties: "IdAplicacionNavigation,IdClaseNavigation,IdToxicologicaNavigation");
                return _mapper.Map<List<GetCaracteristicaDto>>(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteCaracteristicaByItem(int idArticulo, int item)
        {

            try
            {
                var data = _articuloUnitOfWork._caracteristicaRepository.GetAll(p => p.IdArticulo == idArticulo && p.IdItem == item).FirstOrDefault();
                _articuloUnitOfWork._caracteristicaRepository.Delete(data);

                await _articuloUnitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Documento

        public async Task<AddOrEditDocumentoDto> CreateOrUpdateDocumento(AddOrEditDocumentoDto model)
        {

            var documento = _mapper.Map<Documento>(model);
            if (model.IdItem == 0)
            {
                var item = _articuloUnitOfWork._documentoRepository.GetAll(p => p.IdArticulo == model.IdArticulo);
                documento.IdItem = item.Count() == 0 ? 1 : item.Max(p => p.IdItem) + 1;
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

        public async Task<List<GetDocumentoDto>> GetListDocumentoByIdArticulo(int idArticulo)
        {
            try
            {
                var query = _articuloUnitOfWork._documentoRepository.GetAll(p => p.IdArticulo == idArticulo, includeProperties: "IdTipoDocumentoNavigation");
                return _mapper.Map<List<GetDocumentoDto>>(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteDocumentoByItem(int idArticulo, int item)
        {

            try
            {
                var data = _articuloUnitOfWork._documentoRepository.GetAll(p => p.IdArticulo == idArticulo && p.IdItem == item).FirstOrDefault();
                _articuloUnitOfWork._documentoRepository.Delete(data);

                await _articuloUnitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Uso

        public async Task<AddOrEditUsoDto> CreateOrUpdateUso(AddOrEditUsoDto model)
        {

            var uso = _mapper.Map<Uso>(model);
            if (model.IdItem == 0)
            {
                var item = _articuloUnitOfWork._usoRepository.GetAll(p => p.IdArticulo == model.IdArticulo);
                uso.IdItem = item.Count() == 0 ? 1 : item.Max(p => p.IdItem) + 1;
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

        public async Task<List<GetUsoDto>> GetListUsoByIdArticulo(int idArticulo)
        {
            try
            {
                var query = _articuloUnitOfWork._usoRepository.GetAll(p => p.IdArticulo == idArticulo, includeProperties: "IdCultivoNavigation,IdNomCientificoPlagaNavigation");
                var etiqueta = await this.GetEtiquetaDocumento(idArticulo);
                query.ForEach(p =>
                {
                    p.Dosis = etiqueta;
                });

                return _mapper.Map<List<GetUsoDto>>(query);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteUsoByItem(int idArticulo, int item)
        {

            try
            {
                var data = _articuloUnitOfWork._usoRepository.GetAll(p => p.IdArticulo == idArticulo && p.IdItem == item).FirstOrDefault();
                _articuloUnitOfWork._usoRepository.Delete(data);

                await _articuloUnitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetEtiquetaDocumento(int idArticulo)
        {
            try
            {
                var query = _articuloUnitOfWork._documentoRepository.GetAll(p => p.IdArticulo == idArticulo && p.IdTipoDocumento == 3).FirstOrDefault();

                if (query == null)
                {
                    return string.Empty;
                }

                return $"{query.IdItem}-{query.NomDocumento}";

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion


        #region Producto Fabricante

        public async Task<bool> CreateOrUpdateProductoFabricante(List<AddOrEditProductoFabricanteDto> model)
        {

            var listaEntidad = _mapper.Map<List<ProductoFabricante>>(model);

            var resp = _articuloUnitOfWork._productoFabricanteRepository.DeleteForIdProducto(listaEntidad.First().IdArticulo);
            await _articuloUnitOfWork.SaveAsync();


            _articuloUnitOfWork._productoFabricanteRepository.AddRangeProductoFabricante(listaEntidad);
            await _articuloUnitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteProductoFabricanteById(int IdArticulo, int IdFabricante)
        {

            try
            {
                _articuloUnitOfWork._productoFabricanteRepository.Delete(new { IdArticulo, IdFabricante });
                await _articuloUnitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region Producto Formulador

        public async Task<bool> CreateOrUpdateProductoFormulador(List<AddOrEditProductoFormuladorDto> model)
        {

            var listaEntidad = _mapper.Map<List<ProductoFormulador>>(model);

            var resp = _articuloUnitOfWork._productoFormuladorRepository.DeleteForIdProducto(listaEntidad.First().IdProducto);
            await _articuloUnitOfWork.SaveAsync();


            await _articuloUnitOfWork._productoFormuladorRepository.AddRangeProductoFormulador(listaEntidad);
            await _articuloUnitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteProductoFormuladorById(int IdArticulo, int IdFormulador)
        {
            try
            {
                _articuloUnitOfWork._productoFormuladorRepository.Delete(new { IdArticulo, IdFormulador });
                await _articuloUnitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion






    }
}
