using IDCL.AVGUST.SIP.Manager.Pedido;
using IDCL.AVGUST.SIP.ManagerDto.Pedido;
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

        [HttpGet("GetListCostoArticulo/{idEmpresa:int}/{codArticulo?}/{fechaStock?}")]
        public async Task<IActionResult> GetArticulos(int idEmpresa, string? codArticulo = null, string? fechaStock = null)
        {
            return Ok(await _pedidoManager.getListArticulos(idEmpresa, codArticulo, fechaStock));
        }

        [HttpGet("GetListarZonaVendedor/{idEmpresa:int}/{fechaInicio}/{fechaFin}")]
        public async Task<IActionResult> GetListarZonaVendedor(int idEmpresa, string fechaInicio, string fechaFin)
        {
            return Ok(await _pedidoManager.ListarZonaVendedor(idEmpresa, fechaInicio, fechaFin));
        }

        [HttpGet("GetListarSegmentoZona/{idEmpresa:int}/{fechaInicio}/{fechaFin}")]
        public async Task<IActionResult> GetListarSegmentoZona(int idEmpresa, string fechaInicio, string fechaFin)
        {
            return Ok(await _pedidoManager.ListarSegmentoZona(idEmpresa, fechaInicio, fechaFin));
        }

        [HttpGet("GetListarTopCliente/{idEmpresa:int}/{fechaInicio}/{fechaFin}")]
        public async Task<IActionResult> GetListarTopCliente(int idEmpresa, string fechaInicio, string fechaFin)
        {
            return Ok(await _pedidoManager.ListarTopCliente(idEmpresa, fechaInicio, fechaFin));
        }

        [HttpGet("GetListarVentaProducto/{idEmpresa:int}/{fechaInicio}/{fechaFin}")]
        public async Task<IActionResult> GetListarVentaProducto(int idEmpresa, string fechaInicio, string fechaFin)
        {
            return Ok(await _pedidoManager.ListarVentaProducto(idEmpresa, fechaInicio, fechaFin));
        }

        [HttpGet("GetListarVentaClienteProducto/{idEmpresa:int}/{fechaInicio}/{fechaFin}")]
        public async Task<IActionResult> GetListarVentaClienteProducto(int idEmpresa, string fechaInicio, string fechaFin)
        {
            return Ok(await _pedidoManager.ListarVentaClienteProducto(idEmpresa, fechaInicio, fechaFin));
        }


        #region Linea - Credido

        [HttpGet("GetListarLineaCreditoDisponibleZonaCliente/{idEmpresa:int}")]
        public async Task<IActionResult> GetListarLineaCreditoDisponibleZonaCliente(int idEmpresa)
        {
            return Ok(await _pedidoManager.ListarLineaCreditoDisponibleZonaCliente(idEmpresa));
        }

        [HttpGet("GetListarClientesAprobadosLCPorZona/{idEmpresa:int}")]
        public async Task<IActionResult> GetListarClientesAprobadosLCPorZona(int idEmpresa)
        {
            return Ok(await _pedidoManager.ListarClientesAprobadosLCPorZona(idEmpresa));
        }

        [HttpGet("GetListarClientesAtendidosLCPorZona/{idEmpresa:int}")]
        public async Task<IActionResult> GetListarClientesAtendidosLCPorZona(int idEmpresa)
        {
            return Ok(await _pedidoManager.ListarClientesAtendidosLCPorZona(idEmpresa));
        }
        
        [HttpGet("GetListarClientesAtendidosSinLC/{idEmpresa:int}")]
        public async Task<IActionResult> GetListarClientesAtendidosSinLC(int idEmpresa)
        {
            return Ok(await _pedidoManager.ListarClientesAtendidosSinLC(idEmpresa));
        }
        
        [HttpGet("GetListarAvanceCobranzaZonaVendedor/{idEmpresa:int}/{fechaInicio}/{fechaFin}")]
        public async Task<IActionResult> GetListarAvanceCobranzaZonaVendedor(int idEmpresa, string fechaInicio, string fechaFin)
        {
            return Ok(await _pedidoManager.ListarAvanceCobranzaZonaVendedor(idEmpresa, fechaInicio, fechaFin));
        }
        
        [HttpGet("GetListarCtaCteAtrazadaPorZona/{idEmpresa:int}/{fechaFiltro}")]
        public async Task<IActionResult> GetListarCtaCteAtrazadaPorZona(int idEmpresa, string fechaFiltro)
        {
            return Ok(await _pedidoManager.ListarCtaCteAtrazadaPorZona(idEmpresa,fechaFiltro));
        }
        
        [HttpGet("GetListarLetraPorAceptarZona/{idEmpresa:int}")]
        public async Task<IActionResult> GetListarLetraPorAceptarZona(int idEmpresa)
        {
            return Ok(await _pedidoManager.ListarLetraPorAceptarZona(idEmpresa));
        }


        #endregion


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
