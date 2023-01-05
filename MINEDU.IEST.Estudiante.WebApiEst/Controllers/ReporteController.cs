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

        [HttpGet("GetReporteArticulosPlaga/{idUsuario:int}/{filtro?}")]
        public async Task<IActionResult> GetReporteArticulosPlaga(int idUsuario, string filtro)
        {
            return Ok(await _reporteManager.GetArticulosPorPlaga(idUsuario, filtro));
        }

        [HttpGet("GetReporteArticulosCultivo/{idUsuario:int}/{filtro?}")]
        public async Task<IActionResult> GetReporteArticulosCultivo(int idUsuario, string filtro)
        {
            return Ok(await _reporteManager.GetArticulosPorCultivo(idUsuario, filtro));
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
        [Route("GetExcelArticulosPorPlaga/{idUsuario:int}/{filtro?}")]
        public async Task<IActionResult> GetExcelArticulosPorPlaga(int idUsuario, string filtro)
        {
            var query = await _reporteManager.GetExcelArticulosPorPlaga(idUsuario, filtro);

            return File(query, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "articulosByPlaga.xlsx");

        }
        [HttpGet, DisableRequestSizeLimit]
        [Route("GetExcelArticulosPorCultivo/{idUsuario:int}/{filtro?}")]
        public async Task<IActionResult> GetExcelArticulosPorCultivo(int idUsuario, string filtro)
        {
            var query = await _reporteManager.GetExcelArticulosPorCultivo(idUsuario, filtro);

            return File(query, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "articulosByPlaga.xlsx");

        }
        #endregion

    }
}
