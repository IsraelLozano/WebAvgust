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


        [HttpGet("GetReporteArticulosGeneral/{IdUsuario:int}")]
        public async Task<IActionResult> GetArticulos(int IdUsuario)
        {
            return Ok(await _reporteManager.GetArticulosById(IdUsuario));
        }

        [HttpGet("GetReporteArticulosComposicion/{IdUsuario:int}")]
        public async Task<IActionResult> GetReporteArticulosComposicion(int IdUsuario)
        {
            return Ok(await _reporteManager.GetArticulosPorComposicion(IdUsuario));
        }

        [HttpGet("GetReporteArticulosPlaga/{IdUsuario:int}")]
        public async Task<IActionResult> GetReporteArticulosPlaga(int IdUsuario)
        {
            return Ok(await _reporteManager.GetArticulosPorPlaga(IdUsuario));
        }

        [HttpGet("GetReporteArticulosCultivo/{IdUsuario:int}")]
        public async Task<IActionResult> GetReporteArticulosCultivo(int IdUsuario)
        {
            return Ok(await _reporteManager.GetArticulosPorCultivo(IdUsuario));
        }

        #region Excel


        [HttpGet, DisableRequestSizeLimit]
        [Route("GetReporteExcelArticuloGeneral/{IdUsuario:int}")]
        public async Task<IActionResult> GetReporteExcelGeneral(int IdUsuario)
        {
            var query = await _reporteManager.GetExcelArticulosGeneral(IdUsuario);

            return File(query, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "articulosFormulados.xlsx");

        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("GetExcelArticulosPorComposicion/{IdUsuario:int}")]
        public async Task<IActionResult> GetExcelArticulosPorComposicion(int IdUsuario)
        {
            var query = await _reporteManager.GetExcelArticulosPorComposicion(IdUsuario);

            return File(query, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "articulosByComposicion.xlsx");

        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("GetExcelArticulosPorPlaga/{IdUsuario:int}")]
        public async Task<IActionResult> GetExcelArticulosPorPlaga(int IdUsuario)
        {
            var query = await _reporteManager.GetExcelArticulosPorPlaga(IdUsuario);

            return File(query, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "articulosByPlaga.xlsx");

        }
        [HttpGet, DisableRequestSizeLimit]
        [Route("GetExcelArticulosPorCultivo/{IdUsuario:int}")]
        public async Task<IActionResult> GetExcelArticulosPorCultivo(int IdUsuario)
        {
            var query = await _reporteManager.GetExcelArticulosPorCultivo(IdUsuario);

            return File(query, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "articulosByPlaga.xlsx");

        }
        #endregion

    }
}
