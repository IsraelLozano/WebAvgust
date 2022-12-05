using IDCL.AVGUST.SIP.Manager.Maestro;
using IDCL.AVGUST.SIP.ManagerDto.Maestros;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("getListCientificoPlaga")]
        public async Task<IActionResult> getListCientificoPlaga()
        {
            return Ok(await _maestraManager.getListCientificoPlaga());
        }

        [HttpGet("GetCientificoPlagaById/{id}")]
        public async Task<IActionResult> GetCientificoPlagaById(int id)
        {
            return Ok(await _maestraManager.GetCientificoPlagaById(id));
        }

        [HttpPost(template: "CreateOrUpdateCientificoPlaga")]
        public async Task<IActionResult> CreateOrUpdateCientificoPlaga(GetCientificoPlagaDto model)
        {
            return Ok(await _maestraManager.CreateOrUpdateCientificoPlaga(model));
        }

        [HttpGet("deletePlaga/{id}")]
        public async Task<IActionResult> DeletePlaga(int id)
        {
            return Ok(await _maestraManager.AnularCientificoPlaga(id));
        }

        #endregion



        #region Clase

        [HttpGet("getListClase")]
        public async Task<IActionResult> getListClase()
        {
            return Ok(await _maestraManager.getListClase());
        }

        [HttpGet("GetClaseById/{id}")]
        public async Task<IActionResult> GetClaseById(int id)
        {
            return Ok(await _maestraManager.GetClaseById(id));
        }

        [HttpPost(template: "CreateOrUpdateClase")]
        public async Task<IActionResult> CreateOrUpdateClase(GetClaseDto model)
        {
            return Ok(await _maestraManager.CreateOrUpdateClase(model));
        }

        [HttpGet("deleteClase/{id}")]
        public async Task<IActionResult> DeleteClase(int id)
        {
            return Ok(await _maestraManager.AnularClase(id));
        }

        #endregion


        #region Cultivo

        [HttpGet("getListCultivo")]
        public async Task<IActionResult> getListCultivo()
        {
            return Ok(await _maestraManager.getListCultivo());
        }

        [HttpGet("GetCultivoById/{id}")]
        public async Task<IActionResult> GetCultivoById(int id)
        {
            return Ok(await _maestraManager.GetCultivoById(id));
        }

        [HttpPost(template: "CreateOrUpdateCultivo")]
        public async Task<IActionResult> CreateOrUpdateCultivo(GetCultivoDto model)
        {
            return Ok(await _maestraManager.CreateOrUpdateCultivo(model));
        }

        [HttpGet("deleteCultivo/{id}")]
        public async Task<IActionResult> DeleteCultivo(int id)
        {
            return Ok(await _maestraManager.AnularCultivo(id));
        }


        #endregion

        #region Formulador

        [HttpGet("getListFormulador")]
        public async Task<IActionResult> getListFormulador()
        {
            return Ok(await _maestraManager.getListFormulador());
        }

        [HttpGet("GetFormuladorById/{id}")]
        public async Task<IActionResult> GetFormuladorById(int id)
        {
            return Ok(await _maestraManager.GetFormuladorById(id));
        }

        [HttpPost(template: "CreateOrUpdateFormulador")]
        public async Task<IActionResult> CreateOrUpdateFormulador(GetFormuladorDto model)
        {
            return Ok(await _maestraManager.CreateOrUpdateFormulador(model));
        }

        [HttpGet("deleteFormulador/{id}")]
        public async Task<IActionResult> DeleteFormulador(int id)
        {
            return Ok(await _maestraManager.AnularFormulador(id));
        }
        #endregion


        #region GrupoQuimico

        [HttpGet("getListGrupoQuimico")]
        public async Task<IActionResult> getListGrupoQuimico()
        {
            return Ok(await _maestraManager.getListGrupoQuimico());
        }

        [HttpGet("GetGrupoQuimicoById/{id}")]
        public async Task<IActionResult> GetGrupoQuimicoById(int id)
        {
            return Ok(await _maestraManager.GetGrupoQuimicoById(id));
        }

        [HttpPost(template: "CreateOrUpdateGrupoQuimico")]
        public async Task<IActionResult> CreateOrUpdateGrupoQuimico(GetGrupoQuimicoDto model)
        {
            return Ok(await _maestraManager.CreateOrUpdateGrupoQuimico(model));
        }


        [HttpGet("deleteGrupoQuimico/{id}")]
        public async Task<IActionResult> DeleteGrupoQuimico(int id)
        {
            return Ok(await _maestraManager.AnularGrupoQuimico(id));
        }
        #endregion


        #region Id Tipo Produycto

        [HttpGet("getListIdTipoProducto")]
        public async Task<IActionResult> getListIdTipoProducto()
        {
            return Ok(await _maestraManager.getListIdTipoProducto());
        }

        [HttpGet("GetIdTipoProductoById/{id}")]
        public async Task<IActionResult> GetIdTipoProductoById(int id)
        {
            return Ok(await _maestraManager.GetIdTipoProductoById(id));
        }

        [HttpPost(template: "CreateOrUpdateIdTipoProducto")]
        public async Task<IActionResult> CreateOrUpdateIdTipoProducto(GetIdTipoProductoDto model)
        {
            return Ok(await _maestraManager.CreateOrUpdateIdTipoProducto(model));
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

        [HttpGet("getListTitularRegistro")]
        public async Task<IActionResult> getListTitularRegistro()
        {
            return Ok(await _maestraManager.getListTitularRegistro());
        }

        [HttpGet("GetTitularRegistroById/{id}")]
        public async Task<IActionResult> GetTitularRegistroById(int id)
        {
            return Ok(await _maestraManager.GetTitularRegistroById(id));
        }

        [HttpPost(template: "CreateOrUpdateTitularRegistro")]
        public async Task<IActionResult> CreateOrUpdateTitularRegistro(GetTitularRegistroDto model)
        {
            return Ok(await _maestraManager.CreateOrUpdateTitularRegistro(model));
        }


        [HttpGet("deleteTitularRegistro/{id}")]
        public async Task<IActionResult> DeleteTitularRegistro(int id)
        {
            return Ok(await _maestraManager.AnularTitularRegistro(id));
        }

        #endregion


        #region Toxicologica

        [HttpGet("getListToxicologica")]
        public async Task<IActionResult> getListToxicologica()
        {
            return Ok(await _maestraManager.getListToxicologica());
        }

        [HttpGet("GetToxicologicaById/{id}")]
        public async Task<IActionResult> GetToxicologicaById(int id)
        {
            return Ok(await _maestraManager.GetToxicologicaById(id));
        }

        [HttpPost(template: "CreateOrUpdateToxicologica")]
        public async Task<IActionResult> CreateOrUpdateToxicologica(GetToxicologicaDto model)
        {
            return Ok(await _maestraManager.CreateOrUpdateToxicologica(model));
        }


        [HttpGet("deleteToxicologia/{id}")]
        public async Task<IActionResult> DeleteToxicologia(int id)
        {
            return Ok(await _maestraManager.AnularToxicologia(id));
        }
        #endregion


    }
}
