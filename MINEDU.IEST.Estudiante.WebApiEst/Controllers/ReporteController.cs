using IDCL.AVGUST.SIP.Manager.Reporte;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDCL.AVGUST.SIP.WebApiEst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {

        private readonly ILogger<ReporteController> _logger;
        private readonly IReporteManager _reporteManager;

        public ReporteController(ILogger<ReporteController> logger, IReporteManager reporteManager)
        {
            _logger = logger;
            _reporteManager = reporteManager;
        }

        [HttpGet("GetReporteArticulosGeneral/{IdUsuario:int}/{tipoFiltro:int}/{idIngredienteActivo:int}/{filtro?}")]
        public async Task<IActionResult> GetArticulos(int IdUsuario, int tipoFiltro, string filtro = "", int idIngredienteActivo = 0)
        {
            return Ok(await _reporteManager.GetArticulosById(IdUsuario, tipoFiltro, filtro, idIngredienteActivo));
        }

        [HttpGet("GetReporteArticulosComposicion/{IdUsuario:int}/{tipoFiltro:int}/{idIngredienteActivo:int}/{filtro?}")]
        public async Task<IActionResult> GetReporteArticulosComposicion(int IdUsuario, int tipoFiltro, string filtro = "", int idIngredienteActivo = 0)
        {
            return Ok(await _reporteManager.GetArticulosPorComposicion(IdUsuario, tipoFiltro, filtro, idIngredienteActivo));
        }

        [HttpGet("GetReporteArticulosPlaga/{IdUsuario:int}/{tipoFiltro:int}/{idIngredienteActivo:int}/{filtro?}")]
        public async Task<IActionResult> GetReporteArticulosPlaga(int IdUsuario, int tipoFiltro, string filtro = "", int idIngredienteActivo = 0)
        {
            return Ok(await _reporteManager.GetArticulosPorPlaga(IdUsuario, tipoFiltro, filtro, idIngredienteActivo));
        }

        [HttpGet("GetReporteArticulosCultivo/{IdUsuario:int}/{tipoFiltro:int}/{idIngredienteActivo:int}/{filtro?}")]
        public async Task<IActionResult> GetReporteArticulosCultivo(int IdUsuario, int tipoFiltro, string filtro = "", int idIngredienteActivo = 0)
        {
            return Ok(await _reporteManager.GetArticulosPorCultivo(IdUsuario, tipoFiltro, filtro, idIngredienteActivo));
        }

        #region Excel


        [HttpGet, DisableRequestSizeLimit]
        [Route("GetReporteExcelArticuloGeneral/{IdUsuario:int}/{tipoFiltro:int}/{idIngredienteActivo:int}/{filtro?}")]
        public async Task<IActionResult> GetReporteExcelGeneral(int IdUsuario, int tipoFiltro, string filtro = "", int idIngredienteActivo = 0)
        {
            var query = await _reporteManager.GetExcelArticulosGeneral(IdUsuario, tipoFiltro, filtro, idIngredienteActivo);

            return File(query, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "articulosFormulados.xlsx");

        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("GetExcelArticulosPorComposicion/{IdUsuario:int}/{tipoFiltro:int}/{idIngredienteActivo:int}/{filtro?}")]
        public async Task<IActionResult> GetExcelArticulosPorComposicion(int IdUsuario, int tipoFiltro, string filtro = "", int idIngredienteActivo = 0)
        {
            var query = await _reporteManager.GetExcelArticulosPorComposicion(IdUsuario, tipoFiltro, filtro, idIngredienteActivo);

            return File(query, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "articulosByComposicion.xlsx");

        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("GetExcelArticulosPorPlaga/{IdUsuario:int}/{tipoFiltro:int}/{idIngredienteActivo:int}/{filtro?}")]
        public async Task<IActionResult> GetExcelArticulosPorPlaga(int IdUsuario, int tipoFiltro, string filtro = "", int idIngredienteActivo = 0)
        {
            var query = await _reporteManager.GetExcelArticulosPorPlaga(IdUsuario, tipoFiltro, filtro, idIngredienteActivo);

            return File(query, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "articulosByPlaga.xlsx");

        }
        [HttpGet, DisableRequestSizeLimit]
        [Route("GetExcelArticulosPorCultivo/{IdUsuario:int}/{tipoFiltro:int}/{idIngredienteActivo:int}/{filtro?}")]
        public async Task<IActionResult> GetExcelArticulosPorCultivo(int IdUsuario, int tipoFiltro, string filtro = "", int idIngredienteActivo = 0)
        {
            var query = await _reporteManager.GetExcelArticulosPorCultivo(IdUsuario, tipoFiltro, filtro, idIngredienteActivo);

            return File(query, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "articulosByPlaga.xlsx");

        }
        #endregion

    }
}
