using IDCL.AVGUST.SIP.Manager.Articulos;
using IDCL.AVGUST.SIP.ManagerDto.Articulos;
using IDCL.AVGUST.SIP.ManagerDto.Articulos.Add;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MINEDU.IEST.Estudiante.WebApiEst.Controllers;

namespace IDCL.AVGUST.SIP.WebApiEst.Controllers
{
    [ApiController]
    public class ArticuloController : BaseController
    {
        private readonly ILogger<ArticuloController> _logger;
        private readonly IArticuloManager _articuloManager;

        public ArticuloController(ILogger<ArticuloController> logger, IArticuloManager articuloManager)
        {
            _logger = logger;
            this._articuloManager = articuloManager;
        }

        [HttpGet("GetListArticulo")]
        public async Task<IActionResult> GetArticulos()
        {
            return Ok(await _articuloManager.GetListArticulos());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetArticuloByID(int id)
        {
            return Ok(await _articuloManager.GetArticuloById(id));
        }

        [HttpPost("CreateOrUpdateArticulo")]
        public async Task<IActionResult> GetArticuloByID(AddOrEditArticuloDto model)
        {
            return Ok(await _articuloManager.CreateOrUpdateArticulo(model));
        }

        [HttpPost("CreateOrUpdateCaracteristica")]
        public async Task<IActionResult> CreateOrUpdateCaracteristica(AddOrEditCaracteristicaDto model)
        {
            return Ok(await _articuloManager.CreateOrUpdateCaracteristica(model));
        }

        [HttpPost("CreateOrUpdateComposicion")]
        public async Task<IActionResult> CreateOrUpdateComposicion(AddOrEditComposicionDto model)
        {
            return Ok(await _articuloManager.CreateOrUpdateComposicion(model));
        }

        [HttpPost("CreateOrUpdateDocumento")]
        public async Task<IActionResult> CreateOrUpdateDocumento(AddOrEditDocumentoDto model)
        {
            return Ok(await _articuloManager.CreateOrUpdateDocumento(model));
        }

        [HttpPost("CreateOrUpdateUso")]
        public async Task<IActionResult> CreateOrUpdateUso(AddOrEditUsoDto model)
        {
            return Ok(await _articuloManager.CreateOrUpdateUso(model));
        }

    }
}
