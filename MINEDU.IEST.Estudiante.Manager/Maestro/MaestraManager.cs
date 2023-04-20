﻿using AutoMapper;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using IDCL.AVGUST.SIP.ManagerDto.Maestros;
using IDCL.AVGUST.SIP.ManagerDto.Maestros.Add;
using IDCL.AVGUST.SIP.Repository.UnitOfWork;

namespace IDCL.AVGUST.SIP.Manager.Maestro
{
    public class MaestraManager : IMaestraManager
    {
        private readonly IMapper _mapper;
        private readonly MaestraUnitOfWork _maestraUnitOfWork;

        public MaestraManager(IMapper mapper, MaestraUnitOfWork maestraUnitOfWork)
        {
            this._mapper = mapper;
            this._maestraUnitOfWork = maestraUnitOfWork;
        }



        #region Pais

        public async Task<List<GetPaisDto>> getListPais()
        {
            var query = _maestraUnitOfWork._paisRepository.GetAll(p => p.estado, orderBy: p => p.OrderBy(l => l.NomPais));

            return _mapper.Map<List<GetPaisDto>>(query);
        }

        public async Task<GetPaisDto> GetPaisById(int id)
        {
            var query = _maestraUnitOfWork._paisRepository.GetById(id);
            return _mapper.Map<GetPaisDto>(query);
        }

        public async Task<GetPaisDto> CreateOrUpdatePais(GetPaisDto model)
        {
            var entidad = _mapper.Map<Pai>(model);
            if (entidad.IdPais == 0)
            {
                _maestraUnitOfWork._paisRepository.Insert(entidad);
            }
            else
            {
                entidad.estado = true;
                _maestraUnitOfWork._paisRepository.Update(entidad);
            }

            await _maestraUnitOfWork.SaveAsync();

            return _mapper.Map<GetPaisDto>(entidad);
        }

        public async Task<bool> AnularPais(int id)
        {
            var query = _maestraUnitOfWork._paisRepository.GetById(id);
            query.estado = false;
            await _maestraUnitOfWork.SaveAsync();
            return true;
        }
        #endregion


        #region Aplicacion

        public async Task<List<GetAplicacionDto>> getListAplicacion()
        {
            var query = _maestraUnitOfWork._aplicacionRepository.GetAll();

            return _mapper.Map<List<GetAplicacionDto>>(query);
        }

        public async Task<GetAplicacionDto> GetAplicacionById(int id)
        {
            var query = _maestraUnitOfWork._aplicacionRepository.GetById(id);
            return _mapper.Map<GetAplicacionDto>(query);
        }

        public async Task<GetAplicacionDto> CreateOrUpdateAplicacion(GetAplicacionDto model)
        {
            var entidad = _mapper.Map<Aplicacion>(model);
            if (entidad.IdAplicacion == 0)
            {
                _maestraUnitOfWork._aplicacionRepository.Insert(entidad);
            }
            else
            {
                entidad.estado = true;
                _maestraUnitOfWork._aplicacionRepository.Update(entidad);
            }

            await _maestraUnitOfWork.SaveAsync();

            return _mapper.Map<GetAplicacionDto>(entidad);
        }

        public async Task<bool> AnularAplicacion(int id)
        {
            var query = _maestraUnitOfWork._aplicacionRepository.GetById(id);
            query.estado = false;
            await _maestraUnitOfWork.SaveAsync();
            return true;
        }
        #endregion


        #region Cientifico - Plaga

        public async Task<List<GetCientificoPlagaDto>> getListCientificoPlaga()
        {
            var query = _maestraUnitOfWork._cientificoPlagaRepository.GetAll();

            return _mapper.Map<List<GetCientificoPlagaDto>>(query);
        }

        public async Task<GetCientificoPlagaDto> GetCientificoPlagaById(int id)
        {
            var query = _maestraUnitOfWork._cientificoPlagaRepository.GetById(id);
            return _mapper.Map<GetCientificoPlagaDto>(query);
        }

        public async Task<GetCientificoPlagaDto> CreateOrUpdateCientificoPlaga(GetCientificoPlagaDto model)
        {
            var entidad = _mapper.Map<CientificoPlaga>(model);
            if (entidad.IdNomCientificoPlaga == 0)
            {
                _maestraUnitOfWork._cientificoPlagaRepository.Insert(entidad);
            }
            else
            {
                entidad.estado = true;
                _maestraUnitOfWork._cientificoPlagaRepository.Update(entidad);
            }

            await _maestraUnitOfWork.SaveAsync();

            return _mapper.Map<GetCientificoPlagaDto>(entidad);
        }

        public async Task<bool> AnularCientificoPlaga(int id)
        {
            var query = _maestraUnitOfWork._cientificoPlagaRepository.GetById(id);
            query.estado = false;
            await _maestraUnitOfWork.SaveAsync();
            return true;
        }

        #endregion


        #region Clase

        public async Task<List<GetClaseDto>> getListClase()
        {
            var query = _maestraUnitOfWork._claseRepository.GetAll(includeProperties: "IdTipoProductoNavigation");

            return _mapper.Map<List<GetClaseDto>>(query);
        }

        public async Task<GetClaseDto> GetClaseById(int id)
        {
            var query = _maestraUnitOfWork._claseRepository.GetAll(p => p.IdTipoProducto == id, includeProperties: "IdTipoProductoNavigation").FirstOrDefault();
            return _mapper.Map<GetClaseDto>(query);
        }

        public async Task<GetClaseDto> CreateOrUpdateClase(AddClaseDto model)
        {
            var entidad = _mapper.Map<Clase>(model);
            if (entidad.IdClase == 0)
            {
                _maestraUnitOfWork._claseRepository.Insert(entidad);
            }
            else
            {
                entidad.estado = true;
                _maestraUnitOfWork._claseRepository.Update(entidad);
            }

            await _maestraUnitOfWork.SaveAsync();

            return _mapper.Map<GetClaseDto>(entidad);
        }

        public async Task<bool> AnularClase(int id)
        {
            var query = _maestraUnitOfWork._claseRepository.GetById(id);
            query.estado = false;
            await _maestraUnitOfWork.SaveAsync();
            return true;
        }

        #endregion


        #region Cultivo

        public async Task<List<GetCultivoDto>> getListCultivo()
        {
            var query = _maestraUnitOfWork._cultivoRepository.GetAll();

            return _mapper.Map<List<GetCultivoDto>>(query);
        }

        public async Task<GetCultivoDto> GetCultivoById(int id)
        {
            var query = _maestraUnitOfWork._cultivoRepository.GetById(id);
            return _mapper.Map<GetCultivoDto>(query);
        }

        public async Task<GetCultivoDto> CreateOrUpdateCultivo(GetCultivoDto model)
        {
            var entidad = _mapper.Map<Cultivo>(model);
            if (entidad.IdCultivo == 0)
            {
                _maestraUnitOfWork._cultivoRepository.Insert(entidad);
            }
            else
            {
                entidad.estado = true;
                _maestraUnitOfWork._cultivoRepository.Update(entidad);
            }

            await _maestraUnitOfWork.SaveAsync();

            return _mapper.Map<GetCultivoDto>(entidad);
        }

        public async Task<bool> AnularCultivo(int id)
        {
            var query = _maestraUnitOfWork._cultivoRepository.GetById(id);
            query.estado = false;
            await _maestraUnitOfWork.SaveAsync();
            return true;
        }
        #endregion

        #region Formulador

        public async Task<List<GetFormuladorDto>> getListFormulador()
        {
            var query = _maestraUnitOfWork._formuladorRepository.GetAll();

            return _mapper.Map<List<GetFormuladorDto>>(query);
        }

        public async Task<GetFormuladorDto> GetFormuladorById(int id)
        {
            var query = _maestraUnitOfWork._formuladorRepository.GetById(id);
            return _mapper.Map<GetFormuladorDto>(query);
        }

        public async Task<GetFormuladorDto> CreateOrUpdateFormulador(GetFormuladorDto model)
        {
            var entidad = _mapper.Map<Formulador>(model);
            if (entidad.IdFormulador == 0)
            {
                _maestraUnitOfWork._formuladorRepository.Insert(entidad);
            }
            else
            {
                entidad.estado = true;
                _maestraUnitOfWork._formuladorRepository.Update(entidad);
            }

            await _maestraUnitOfWork.SaveAsync();

            return _mapper.Map<GetFormuladorDto>(entidad);
        }

        public async Task<bool> AnularFormulador(int id)
        {
            var query = _maestraUnitOfWork._formuladorRepository.GetById(id);
            query.estado = false;
            await _maestraUnitOfWork.SaveAsync();
            return true;
        }

        #endregion


        #region GrupoQuimico

        public async Task<List<GetGrupoQuimicoDto>> getListGrupoQuimico()
        {
            var query = _maestraUnitOfWork._grupoQuimicoRepository.GetAll();

            return _mapper.Map<List<GetGrupoQuimicoDto>>(query);
        }

        public async Task<GetGrupoQuimicoDto> GetGrupoQuimicoById(int id)
        {
            var query = _maestraUnitOfWork._grupoQuimicoRepository.GetById(id);
            return _mapper.Map<GetGrupoQuimicoDto>(query);
        }

        public async Task<GetGrupoQuimicoDto> CreateOrUpdateGrupoQuimico(GetGrupoQuimicoDto model)
        {
            var entidad = _mapper.Map<GrupoQuimico>(model);
            if (entidad.IdGrupoQuimico == 0)
            {
                _maestraUnitOfWork._grupoQuimicoRepository.Insert(entidad);
            }
            else
            {
                entidad.estado = true;
                _maestraUnitOfWork._grupoQuimicoRepository.Update(entidad);
            }

            await _maestraUnitOfWork.SaveAsync();

            return _mapper.Map<GetGrupoQuimicoDto>(entidad);
        }

        public async Task<bool> AnularGrupoQuimico(int id)
        {
            var query = _maestraUnitOfWork._grupoQuimicoRepository.GetById(id);
            query.estado = false;
            await _maestraUnitOfWork.SaveAsync();
            return true;
        }

        #endregion

        #region Id Tipo Produycto

        public async Task<List<GetIdTipoProductoDto>> getListIdTipoProducto()
        {
            var query = _maestraUnitOfWork._tipoProductoRepository.GetAll();

            return _mapper.Map<List<GetIdTipoProductoDto>>(query);
        }

        public async Task<GetIdTipoProductoDto> GetIdTipoProductoById(int id)
        {
            var query = _maestraUnitOfWork._tipoProductoRepository.GetById(id);
            return _mapper.Map<GetIdTipoProductoDto>(query);
        }

        public async Task<GetIdTipoProductoDto> CreateOrUpdateIdTipoProducto(GetIdTipoProductoDto model)
        {
            var entidad = _mapper.Map<IdTipoProducto>(model);
            if (entidad.IdTipoProducto1 == 0)
            {
                _maestraUnitOfWork._tipoProductoRepository.Insert(entidad);
            }
            else
            {
                entidad.estado = true;
                _maestraUnitOfWork._tipoProductoRepository.Update(entidad);
            }

            await _maestraUnitOfWork.SaveAsync();

            return _mapper.Map<GetIdTipoProductoDto>(entidad);
        }

        public async Task<bool> AnularTipoProducto(int id)
        {
            var query = _maestraUnitOfWork._tipoProductoRepository.GetById(id);
            query.estado = false;
            await _maestraUnitOfWork.SaveAsync();
            return true;
        }

        #endregion


        #region Tipo Documento

        public async Task<List<GetTipoDocumentoDto>> getListTipoDocumento()
        {
            var query = _maestraUnitOfWork._tipoDocumentoRepository.GetAll();

            return _mapper.Map<List<GetTipoDocumentoDto>>(query);
        }

        public async Task<GetTipoDocumentoDto> GetTipoDocumentoById(int id)
        {
            var query = _maestraUnitOfWork._tipoDocumentoRepository.GetById(id);
            return _mapper.Map<GetTipoDocumentoDto>(query);
        }

        public async Task<GetTipoDocumentoDto> CreateOrUpdateTipoDocumento(GetTipoDocumentoDto model)
        {
            var entidad = _mapper.Map<TipoDocumento>(model);
            if (entidad.IdTipoDocumento == 0)
            {
                _maestraUnitOfWork._tipoDocumentoRepository.Insert(entidad);
            }
            else
            {
                entidad.estado = true;
                _maestraUnitOfWork._tipoDocumentoRepository.Update(entidad);
            }

            await _maestraUnitOfWork.SaveAsync();

            return _mapper.Map<GetTipoDocumentoDto>(entidad);
        }

        public async Task<bool> AnularTipoDocumento(int id)
        {
            var query = _maestraUnitOfWork._tipoDocumentoRepository.GetById(id);
            query.estado = false;
            await _maestraUnitOfWork.SaveAsync();
            return true;
        }

        #endregion

        #region Titular registros

        public async Task<List<GetTitularRegistroDto>> getListTitularRegistro()
        {
            var query = _maestraUnitOfWork._titularRepository.GetAll();

            return _mapper.Map<List<GetTitularRegistroDto>>(query);
        }

        public async Task<GetTitularRegistroDto> GetTitularRegistroById(int id)
        {
            var query = _maestraUnitOfWork._titularRepository.GetById(id);
            return _mapper.Map<GetTitularRegistroDto>(query);
        }

        public async Task<GetTitularRegistroDto> CreateOrUpdateTitularRegistro(GetTitularRegistroDto model)
        {
            var entidad = _mapper.Map<TitularRegistro>(model);
            if (entidad.IdTitularRegistro == 0)
            {
                _maestraUnitOfWork._titularRepository.Insert(entidad);
            }
            else
            {
                entidad.estado = true;
                _maestraUnitOfWork._titularRepository.Update(entidad);
            }

            await _maestraUnitOfWork.SaveAsync();

            return _mapper.Map<GetTitularRegistroDto>(entidad);
        }

        public async Task<bool> AnularTitularRegistro(int id)
        {
            var query = _maestraUnitOfWork._titularRepository.GetById(id);
            query.estado = false;
            await _maestraUnitOfWork.SaveAsync();
            return true;
        }

        #endregion


        #region Toxicologica

        public async Task<List<GetToxicologicaDto>> getListToxicologica()
        {
            var query = _maestraUnitOfWork._toxicologicaRepository.GetAll();

            return _mapper.Map<List<GetToxicologicaDto>>(query);
        }

        public async Task<GetToxicologicaDto> GetToxicologicaById(int id)
        {
            var query = _maestraUnitOfWork._toxicologicaRepository.GetById(id);
            return _mapper.Map<GetToxicologicaDto>(query);
        }

        public async Task<GetToxicologicaDto> CreateOrUpdateToxicologica(GetToxicologicaDto model)
        {
            var entidad = _mapper.Map<Toxicologica>(model);
            if (entidad.IdToxicologica == 0)
            {
                _maestraUnitOfWork._toxicologicaRepository.Insert(entidad);
            }
            else
            {
                entidad.estado = true;
                _maestraUnitOfWork._toxicologicaRepository.Update(entidad);
            }

            await _maestraUnitOfWork.SaveAsync();

            return _mapper.Map<GetToxicologicaDto>(entidad);
        }

        public async Task<bool> AnularToxicologia(int id)
        {
            var query = _maestraUnitOfWork._toxicologicaRepository.GetById(id);
            query.estado = false;
            await _maestraUnitOfWork.SaveAsync();
            return true;
        }

        #endregion

        #region Tipo Formulacion

        public async Task<List<GetTipoFormulacionDto>> getListTipoFormulacion()
        {
            var query = _maestraUnitOfWork._tipoFormulacionRepository.GetAll();

            return _mapper.Map<List<GetTipoFormulacionDto>>(query);
        }

        public async Task<GetTipoFormulacionDto> GetTipoFormulacionById(int id)
        {
            var query = _maestraUnitOfWork._tipoFormulacionRepository.GetById(id);
            return _mapper.Map<GetTipoFormulacionDto>(query);
        }

        public async Task<GetTipoFormulacionDto> CreateOrUpdateTipoFormulacion(GetTipoFormulacionDto model)
        {
            try
            {
                var entidad = _mapper.Map<TipoFormulacion>(model);
                if (entidad.IdTipoFormulacion == 0)
                {
                    _maestraUnitOfWork._tipoFormulacionRepository.Insert(entidad);
                }
                else
                {
                    entidad.estado = true;
                    _maestraUnitOfWork._tipoFormulacionRepository.Update(entidad);
                }

                await _maestraUnitOfWork.SaveAsync();

                return _mapper.Map<GetTipoFormulacionDto>(entidad);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> AnularTipoFormulacion(int id)
        {
            var query = _maestraUnitOfWork._tipoFormulacionRepository.GetById(id);
            query.estado = false;
            await _maestraUnitOfWork.SaveAsync();
            return true;
        }

        #endregion


        #region Ingrediente Activo

        public async Task<List<GetTipoIngredienteActivoDto>> getListTipoIngredienteActivo()
        {
            var query = _maestraUnitOfWork._ingredienteActivoRepository.GetAll();

            return _mapper.Map<List<GetTipoIngredienteActivoDto>>(query);
        }

        public async Task<GetTipoIngredienteActivoDto> GetTipoIngredienteActivoById(int id)
        {
            var query = _maestraUnitOfWork._ingredienteActivoRepository.GetById(id);
            return _mapper.Map<GetTipoIngredienteActivoDto>(query);
        }

        public async Task<GetTipoIngredienteActivoDto> CreateOrUpdateTipoIngredienteActivo(GetTipoIngredienteActivoDto model)
        {
            try
            {
                var entidad = _mapper.Map<IngredienteActivo>(model);
                if (entidad.IngredenteActivo == 0)
                {
                    _maestraUnitOfWork._ingredienteActivoRepository.Insert(entidad);
                }
                else
                {
                    entidad.estado = true;
                    _maestraUnitOfWork._ingredienteActivoRepository.Update(entidad);
                }

                await _maestraUnitOfWork.SaveAsync();

                return _mapper.Map<GetTipoIngredienteActivoDto>(entidad);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> AnularTipoIngredienteActivo(int id)
        {
            var query = _maestraUnitOfWork._ingredienteActivoRepository.GetById(id);
            query.estado = false;
            await _maestraUnitOfWork.SaveAsync();
            return true;
        }

        #endregion


        #region Fabricante


        public async Task<List<GetFabricanteDto>> getListFabricante()
        {
            var query = _maestraUnitOfWork._fabricanteRepository.GetAll();

            return _mapper.Map<List<GetFabricanteDto>>(query);
        }

        public async Task<GetFabricanteDto> GetFabricanteById(int id)
        {
            var query = _maestraUnitOfWork._fabricanteRepository.GetById(id);
            return _mapper.Map<GetFabricanteDto>(query);
        }

        public async Task<GetFabricanteDto> CreateOrUpdateFabricante(GetFabricanteDto model)
        {
            try
            {
                var entidad = _mapper.Map<Fabricante>(model);
                if (entidad.IdFabricante == 0)
                {
                    _maestraUnitOfWork._fabricanteRepository.Insert(entidad);
                }
                else
                {
                    entidad.Estado = true;
                    _maestraUnitOfWork._fabricanteRepository.Update(entidad);
                }

                await _maestraUnitOfWork.SaveAsync();

                return _mapper.Map<GetFabricanteDto>(entidad);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> AnularFabricante(int id)
        {
            var query = _maestraUnitOfWork._fabricanteRepository.GetById(id);
            query.Estado = false;
            await _maestraUnitOfWork.SaveAsync();
            return true;
        }


        #endregion
    }
}
