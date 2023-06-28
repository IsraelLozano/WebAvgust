using IDCL.AVGUST.SIP.Manager.Maestro;
using IDCL.AVGUST.SIP.ManagerDto.Maestros;
using IDCL.AVGUST.SIP.ManagerDto.Maestros.Add;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MINEDU.IEST.Estudiante.Inf_Utils.Helpers;
using MINEDU.IEST.Estudiante.WebApiEst.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace IDCL.AVGUST.SIP.WebApiEst.Controllers
{
    [ApiController]
    [SwaggerTag("Apis para maestras")]
    public class MaestraController : BaseController
    {
        private readonly ILogger<MaestraController> _logger;
        private readonly IMaestraManager _maestraManager;

        public MaestraController(ILogger<MaestraController> logger, IMaestraManager maestraManager)
        {
            this._logger = logger;
            this._maestraManager = maestraManager;
        }


        #region Pais
        [SwaggerOperation(
         Summary = "Obtiene una lista de paises"
        )]
        [HttpGet("getListPais")]
        public async Task<IActionResult> getListPais()
        {
            return Ok(await _maestraManager.getListPais());
        }

        [HttpGet("GetPaisById/{id}")]
        public async Task<IActionResult> GetPaisById(int id)
        {
            return Ok(await _maestraManager.GetPaisById(id));
        }

        [HttpPost(template: "CreateOrUpdatePais")]
        public async Task<IActionResult> CreateOrUpdatePais(GetPaisDto model)
        {
            return Ok(await _maestraManager.CreateOrUpdatePais(model));
        }


        [HttpGet("deletePais/{id}")]
        public async Task<IActionResult> DeletePais(int id)
        {
            return Ok(await _maestraManager.AnularPais(id));
        }

        #endregion


        #region Aplicacion

        [HttpGet("getListAplicacion")]
        public async Task<IActionResult> getListAplicacion()
        {
            return Ok(await _maestraManager.getListAplicacion());
        }

        [HttpGet("GetAplicacionById/{id}")]
        public async Task<IActionResult> GetAplicacionById(int id)
        {
            return Ok(await _maestraManager.GetAplicacionById(id));
        }

        [HttpPost(template: "CreateOrUpdateAplicacion")]
        public async Task<IActionResult> CreateOrUpdateAplicacion(GetAplicacionDto model)
        {
            return Ok(await _maestraManager.CreateOrUpdateAplicacion(model));
        }

        [HttpGet("deleteAplicacion/{id}")]
        public async Task<IActionResult> DeleteAplicacion(int id)
        {
            return Ok(await _maestraManager.AnularAplicacion(id));
        }
        #endregion


        #region Cientifico - Plaga

        [HttpGet("getListCientificoPlaga/{filter?}")]
        public async Task<IActionResult> getListCientificoPlaga(string filter = "")
        {
            return Ok(await _maestraManager.getListCientificoPlaga(filter));
        }

        [HttpGet("GetCientificoPlagaById/{id}")]
        public async Task<IActionResult> GetCientificoPlagaById(int id)
        {
            return Ok(await _maestraManager.GetCientificoPlagaById(id));
        }

        [HttpPost(template: "CreateOrUpdateCientificoPlaga")]
        public async Task<IActionResult> CreateOrUpdateCientificoPlaga(GetCientificoPlagaDto model)
        {
            try
            {
                var response = await _maestraManager.CreateOrUpdateCientificoPlaga(model);
                if (response.EsError)
                {
                    ModelState.AddModelError("validacion", response.MensajeError);
                    return UnprocessableEntity(ExtensionTools.Validaciones(ModelState));
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw ex;
            }

        }

        [HttpGet("deletePlaga/{id}")]
        public async Task<IActionResult> DeletePlaga(int id)
        {
            return Ok(await _maestraManager.AnularCientificoPlaga(id));
        }

        #endregion



        #region Clase

        [HttpGet("getListClase/{filter?}")]
        public async Task<IActionResult> getListClase(string filter = "")
        {
            return Ok(await _maestraManager.getListClase(filter));
        }

        [HttpGet("GetClaseById/{id}")]
        public async Task<IActionResult> GetClaseById(int id)
        {
            return Ok(await _maestraManager.GetClaseById(id));
        }

        [HttpPost(template: "CreateOrUpdateClase")]
        public async Task<IActionResult> CreateOrUpdateClase(AddClaseDto model)
        {
            try
            {
                var response = await _maestraManager.CreateOrUpdateClase(model);
                if (response.EsError)
                {
                    ModelState.AddModelError("validacion", response.MensajeError);
                    return UnprocessableEntity(ExtensionTools.Validaciones(ModelState));
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw ex;
            }
        }

        [HttpGet("deleteClase/{id}")]
        public async Task<IActionResult> DeleteClase(int id)
        {
            return Ok(await _maestraManager.AnularClase(id));
        }

        #endregion


        #region Cultivo

        [HttpGet("getListCultivo/{filter?}")]
        public async Task<IActionResult> getListCultivo(string filter = "")
        {
            return Ok(await _maestraManager.getListCultivo(filter));
        }

        [HttpGet("GetCultivoById/{id}")]
        public async Task<IActionResult> GetCultivoById(int id)
        {
            return Ok(await _maestraManager.GetCultivoById(id));
        }

        [HttpPost(template: "CreateOrUpdateCultivo")]
        public async Task<IActionResult> CreateOrUpdateCultivo(GetCultivoDto model)
        {
            try
            {
                var response = await _maestraManager.CreateOrUpdateCultivo(model);
                if (response.EsError)
                {
                    ModelState.AddModelError("validacion", response.MensajeError);
                    return UnprocessableEntity(ExtensionTools.Validaciones(ModelState));
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw ex;
            }

        }

        [HttpGet("deleteCultivo/{id}")]
        public async Task<IActionResult> DeleteCultivo(int id)
        {
            return Ok(await _maestraManager.AnularCultivo(id));
        }


        #endregion

        #region Formulador

        [HttpGet("getListFormulador/{filter?}")]
        public async Task<IActionResult> getListFormulador(string filter = "")
        {
            return Ok(await _maestraManager.getListFormulador(filter));
        }

        [HttpGet("GetFormuladorById/{id}")]
        public async Task<IActionResult> GetFormuladorById(int id)
        {
            return Ok(await _maestraManager.GetFormuladorById(id));
        }

        [HttpPost(template: "CreateOrUpdateFormulador")]
        public async Task<IActionResult> CreateOrUpdateFormulador(GetFormuladorDto model)
        {
            try
            {
                var response = await _maestraManager.CreateOrUpdateFormulador(model);
                if (response.EsError)
                {
                    ModelState.AddModelError("validacion", response.MensajeError);
                    return UnprocessableEntity(ExtensionTools.Validaciones(ModelState));
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw ex;
            }

        }

        [HttpGet("deleteFormulador/{id}")]
        public async Task<IActionResult> DeleteFormulador(int id)
        {
            return Ok(await _maestraManager.AnularFormulador(id));
        }
        #endregion


        #region GrupoQuimico

        [HttpGet("getListGrupoQuimico/{filter?}")]
        public async Task<IActionResult> getListGrupoQuimico(string filter = "")
        {
            return Ok(await _maestraManager.getListGrupoQuimico(filter));
        }

        [HttpGet("GetGrupoQuimicoById/{id}")]
        public async Task<IActionResult> GetGrupoQuimicoById(int id)
        {
            return Ok(await _maestraManager.GetGrupoQuimicoById(id));
        }

        [HttpPost(template: "CreateOrUpdateGrupoQuimico")]
        public async Task<IActionResult> CreateOrUpdateGrupoQuimico(GetGrupoQuimicoDto model)
        {
            try
            {
                var response = await _maestraManager.CreateOrUpdateGrupoQuimico(model);
                if (response.EsError)
                {
                    ModelState.AddModelError("validacion", response.MensajeError);
                    return UnprocessableEntity(ExtensionTools.Validaciones(ModelState));
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw ex;
            }

        }


        [HttpGet("deleteGrupoQuimico/{id}")]
        public async Task<IActionResult> DeleteGrupoQuimico(int id)
        {
            return Ok(await _maestraManager.AnularGrupoQuimico(id));
        }
        #endregion


        #region Id Tipo Produycto

        [HttpGet("getListIdTipoProducto/{filter?}")]
        public async Task<IActionResult> getListIdTipoProducto(string filter = "")
        {
            return Ok(await _maestraManager.getListIdTipoProducto(filter));
        }

        [HttpGet("GetIdTipoProductoById/{id}")]
        public async Task<IActionResult> GetIdTipoProductoById(int id)
        {
            return Ok(await _maestraManager.GetIdTipoProductoById(id));
        }

        [HttpPost(template: "CreateOrUpdateIdTipoProducto")]
        public async Task<IActionResult> CreateOrUpdateIdTipoProducto(GetIdTipoProductoDto model)
        {
            try
            {
                var response = await _maestraManager.CreateOrUpdateIdTipoProducto(model);
                if (response.EsError)
                {
                    ModelState.AddModelError("validacion", response.MensajeError);
                    return UnprocessableEntity(ExtensionTools.Validaciones(ModelState));
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw ex;
            }

        }


        [HttpGet("deleteIdTipoProducto/{id}")]
        public async Task<IActionResult> DeleteIdTipoProducto(int id)
        {
            return Ok(await _maestraManager.AnularTipoProducto(id));
        }
        #endregion


        #region Tipo Documento

        [HttpGet("getListTipoDocumento")]
        public async Task<IActionResult> getListTipoDocumento()
        {
            return Ok(await _maestraManager.getListTipoDocumento());
        }

        [HttpGet("GetTipoDocumentoById/{id}")]
        public async Task<IActionResult> GetTipoDocumentoById(int id)
        {
            return Ok(await _maestraManager.GetTipoDocumentoById(id));
        }

        [HttpPost(template: "CreateOrUpdateTipoDocumento")]
        public async Task<IActionResult> CreateOrUpdateTipoDocumento(GetTipoDocumentoDto model)
        {
            return Ok(await _maestraManager.CreateOrUpdateTipoDocumento(model));
        }


        [HttpGet("deleteTipoDocumento/{id}")]
        public async Task<IActionResult> DeleteTipoDocumento(int id)
        {
            return Ok(await _maestraManager.AnularTipoDocumento(id));
        }

        #endregion


        #region Titular registros

        [HttpGet("getListTitularRegistro/{filter?}")]
        public async Task<IActionResult> getListTitularRegistro(string filter = "")
        {
            return Ok(await _maestraManager.getListTitularRegistro(filter));
        }

        [HttpGet("GetTitularRegistroById/{id}")]
        public async Task<IActionResult> GetTitularRegistroById(int id)
        {
            return Ok(await _maestraManager.GetTitularRegistroById(id));
        }

        [HttpPost(template: "CreateOrUpdateTitularRegistro")]
        public async Task<IActionResult> CreateOrUpdateTitularRegistro(GetTitularRegistroDto model)
        {
            try
            {
                var response = await _maestraManager.CreateOrUpdateTitularRegistro(model);
                if (response.EsError)
                {
                    ModelState.AddModelError("validacion", response.MensajeError);
                    return UnprocessableEntity(ExtensionTools.Validaciones(ModelState));
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw ex;
            }
        }


        [HttpGet("deleteTitularRegistro/{id}")]
        public async Task<IActionResult> DeleteTitularRegistro(int id)
        {
            return Ok(await _maestraManager.AnularTitularRegistro(id));
        }

        #endregion


        #region Toxicologica

        [HttpGet("getListToxicologica/{filter?}")]
        public async Task<IActionResult> getListToxicologica(string filter = "")
        {
            return Ok(await _maestraManager.getListToxicologica(filter));
        }

        [HttpGet("GetToxicologicaById/{id}")]
        public async Task<IActionResult> GetToxicologicaById(int id)
        {
            return Ok(await _maestraManager.GetToxicologicaById(id));
        }

        [HttpPost(template: "CreateOrUpdateToxicologica")]
        public async Task<IActionResult> CreateOrUpdateToxicologica(GetToxicologicaDto model)
        {
            try
            {
                var response = await _maestraManager.CreateOrUpdateToxicologica(model);
                if (response.EsError)
                {
                    ModelState.AddModelError("validacion", response.MensajeError);
                    return UnprocessableEntity(ExtensionTools.Validaciones(ModelState));
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw ex;
            }
        }


        [HttpGet("deleteToxicologia/{id}")]
        public async Task<IActionResult> DeleteToxicologia(int id)
        {
            return Ok(await _maestraManager.AnularToxicologia(id));
        }
        #endregion


        #region Tipo Formulacion

        [HttpGet("getListTipoFormulacion/{filter?}")]
        public async Task<IActionResult> getListTipoFormulacion(string filter = "")
        {
            return Ok(await _maestraManager.getListTipoFormulacion(filter));
        }

        [HttpGet("GetTipoFormulacionById/{id}")]
        public async Task<IActionResult> GetTipoFormulacionById(int id)
        {
            return Ok(await _maestraManager.GetTipoFormulacionById(id));
        }

        [HttpPost(template: "CreateOrUpdateTipoFormulacion")]
        public async Task<IActionResult> CreateOrUpdateTipoFormulacion(GetTipoFormulacionDto model)
        {
            try
            {
                var response = await _maestraManager.CreateOrUpdateTipoFormulacion(model);
                if (response.EsError)
                {
                    ModelState.AddModelError("validacion", response.MensajeError);
                    return UnprocessableEntity(ExtensionTools.Validaciones(ModelState));
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw ex;
            }
        }

        [HttpGet("deleteTipoFormulacion/{id}")]
        public async Task<IActionResult> DeleteTipoFormulacion(int id)
        {
            return Ok(await _maestraManager.AnularToxicologia(id));
        }
        #endregion


        #region IngredienteActivo

        [HttpGet("getListTipoIngredienteActivo/{filter?}")]
        public async Task<IActionResult> getListTipoIngredienteActivo(string filter = "")
        {
            return Ok(await _maestraManager.getListTipoIngredienteActivo(filter));
        }

        [HttpGet("GetTipoIngredienteActivoById/{id}")]
        public async Task<IActionResult> GetTipoIngredienteActivoById(int id)
        {
            return Ok(await _maestraManager.GetTipoIngredienteActivoById(id));
        }

        [HttpPost(template: "CreateOrUpdateTipoIngredienteActivo")]
        public async Task<IActionResult> CreateOrUpdateTipoIngredienteActivo(GetTipoIngredienteActivoDto model)
        {
            try
            {
                var response = await _maestraManager.CreateOrUpdateTipoIngredienteActivo(model);
                if (response.EsError)
                {
                    ModelState.AddModelError("validacion", response.MensajeError);
                    return UnprocessableEntity(ExtensionTools.Validaciones(ModelState));
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw ex;
            }

        }

        [HttpGet("deleteIngredienteActivo/{id}")]
        public async Task<IActionResult> DeleteTipoIngredienteActivo(int id)
        {
            return Ok(await _maestraManager.AnularTipoIngredienteActivo(id));
        }
        #endregion

        #region Fabridante

        [HttpGet("getListFabricante/{filter?}")]
        public async Task<IActionResult> getListFabricante(string filter = "")
        {
            return Ok(await _maestraManager.getListFabricante(filter));



        }

        [HttpGet("GetFabricanteById/{id}")]
        public async Task<IActionResult> GetFabricanteById(int id)
        {
            return Ok(await _maestraManager.GetFabricanteById(id));
        }

        [HttpPost(template: "CreateOrUpdateFabricante")]
        public async Task<IActionResult> CreateOrUpdateFabricante(GetFabricanteDto model)
        {

            try
            {
                var response = await _maestraManager.CreateOrUpdateFabricante(model);
                if (response.EsError)
                {
                    ModelState.AddModelError("validacion", response.MensajeError);
                    return UnprocessableEntity(ExtensionTools.Validaciones(ModelState));
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw ex;
            }

        }

        [HttpGet("deleteFabricante/{id}")]
        public async Task<IActionResult> DeleteFabricante(int id)
        {
            return Ok(await _maestraManager.AnularFabricante(id));
        }
        #endregion


    }
}
