using IDCL.AVGUST.SIP.Manager.Articulos;
using IDCL.AVGUST.SIP.Manager.Pedido;
using IDCL.AVGUST.SIP.ManagerDto.Pedido;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MINEDU.IEST.Estudiante.Inf_Utils.Helpers;

namespace IDCL.AVGUST.SIP.PedidoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly IPedidoManager _pedidoManager;

        public PedidoController(ILogger<PedidoController> logger, IPedidoManager pedidoManager)
        {
            _logger = logger;
            _pedidoManager = pedidoManager;
        }

        [HttpGet("GetListCostoArticulo/{idEmpresa:int}/{codArticulo?}")]
        public async Task<IActionResult> GetArticulos(int idEmpresa, string? codArticulo = null)
        {
            return Ok(await _pedidoManager.getListArticulos(idEmpresa, codArticulo));
        }

        [HttpPost("AddPedido")]
        public async Task<IActionResult> AddPedido(AddPedidoDto model)
        {
            try
            {
                var response = await _pedidoManager.AddPedido(model);
                if (response.EsError)
                {
                    ModelState.AddModelError("validacion", response.MensajeError);
                    return UnprocessableEntity(ExtensionTools.Validaciones(ModelState));
                }
                return Ok(new { idPedido = response.IdFacturar });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("validacion", ex.Message);
                UnprocessableEntity(ExtensionTools.Validaciones(ModelState));
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
