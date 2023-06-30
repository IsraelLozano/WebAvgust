using IDCL.AVGUST.SIP.Manager.Articulos;
using IDCL.AVGUST.SIP.Manager.Calculator;
using IDCL.AVGUST.SIP.ManagerDto.Calculator.Simulador;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDLC.AVGUST.SIP.Calc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;
        private readonly IArticuloCalculatorManager _articuloCalculatorManager;
        private readonly ISimuladorPedidoManager _simuladorPedidoManager;

        public CalculatorController(ILogger<CalculatorController> logger, IArticuloCalculatorManager articuloCalculatorManager, ISimuladorPedidoManager simuladorPedidoManager)
        {
            _logger = logger;
            _articuloCalculatorManager = articuloCalculatorManager;
            this._simuladorPedidoManager = simuladorPedidoManager;
        }

        [HttpGet("GetArticuloAll/{filter?}")]
        public async Task<IActionResult> GetArticulosAll(string filter = "")
        {
            return Ok(await _articuloCalculatorManager.GetArticuloCals(filter));
        }

        #region Simulador Pedidos


        [HttpGet("GetPedidoAll")]
        public async Task<IActionResult> getPedido()
        {
            return Ok(await _simuladorPedidoManager.getListPedidoAll());
        }

        [HttpGet("GetPedidoById/{id:int}")]
        public async Task<IActionResult> GetPedidoById(int id)
        {
            return Ok(await _simuladorPedidoManager.GetPedidoById(id));
        }

        [HttpPost("AddOrEditPedido")]
        public async Task<IActionResult> addPedido(GetPedidoDto request)
        {
            return Ok(await _simuladorPedidoManager.addPedido(request));
        }




        #endregion
    }
}
