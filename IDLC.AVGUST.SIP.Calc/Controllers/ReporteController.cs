using IDCL.AVGUST.SIP.Manager.Reporte;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDLC.AVGUST.SIP.Calc.Controllers
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


  
        [HttpGet, DisableRequestSizeLimit]
        [Route("GetExcelCalculatorById/{idPedido:int}")]
        public async Task<IActionResult> GetExcelCalculatorById(int idPedido)
        {
            var query = await _reporteManager.GetExcelCalculatorById(idPedido);

            return File(query, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "GetExcelCalculatorById.xlsx");

        }
    }
}
