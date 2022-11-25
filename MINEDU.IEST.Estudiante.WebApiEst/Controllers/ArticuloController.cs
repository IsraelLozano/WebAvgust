using IDCL.AVGUST.SIP.Manager.Articulos;
using IDCL.AVGUST.SIP.ManagerDto.Articulos;
using IDCL.AVGUST.SIP.ManagerDto.Articulos.Add;
using Microsoft.AspNetCore.Cors;
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

        #region Caracteristicas
        [HttpPost("CreateOrUpdateCaracteristica")]
        public async Task<IActionResult> CreateOrUpdateCaracteristica(AddOrEditCaracteristicaDto model)
        {
            return Ok(await _articuloManager.CreateOrUpdateCaracteristica(model));
        }

        [HttpGet("{id:int}/caracteristicas")]
        public async Task<IActionResult> GetCaracteristicaByIdArticulo(int id)
        {
            return Ok(await _articuloManager.GetListCaracteristicaByIdArticulo(id));
        }

        [HttpGet("deleteCaracteristica/{IdArticulo:int}/{item:int}")]
        public async Task<IActionResult> DeleteCaracteristicaByItem(int IdArticulo, int item)
        {
            return Ok(await _articuloManager.DeleteCaracteristicaByItem(IdArticulo, item));
        }


        #endregion

        #region Composicion

        [HttpPost("CreateOrUpdateComposicion")]
        public async Task<IActionResult> CreateOrUpdateComposicion(AddOrEditComposicionDto model)
        {
            return Ok(await _articuloManager.CreateOrUpdateComposicion(model));
        }

        [HttpGet("deleteComposicion/{IdArticulo:int}/{item:int}")]
        //[EnableCors("AllowAllCORS")]
        //[HttpDelete("{IdArticulo:int}")]
        public async Task<IActionResult> DeleteComposicionByItem(int IdArticulo, int item)
        {
            return Ok(await _articuloManager.DeleteComposicionByItem(IdArticulo, item));
        }
        [HttpGet("{id:int}/composiciones")]
        public async Task<IActionResult> GetComposicionByIdArticulo(int id)
        {
            return Ok(await _articuloManager.GetListComposicionByIdArticulo(id));
        }

        #endregion


        #region Documentos
        [HttpPost("CreateOrUpdateDocumento")]
        public async Task<IActionResult> CreateOrUpdateDocumento(AddOrEditDocumentoDto model)
        {
            return Ok(await _articuloManager.CreateOrUpdateDocumento(model));
        }

        [HttpGet("deleteDocumento/{IdArticulo:int}/{item:int}")]
        public async Task<IActionResult> DeleteDocumentoByItem(int IdArticulo, int item)
        {
            return Ok(await _articuloManager.DeleteDocumentoByItem(IdArticulo, item));
        }
        [HttpGet("{id:int}/documentos")]
        public async Task<IActionResult> GetDocumentoByIdArticulo(int id)
        {
            return Ok(await _articuloManager.GetListDocumentoByIdArticulo(id));
        }

        #endregion

        #region Usos
        [HttpPost("CreateOrUpdateUso")]
        public async Task<IActionResult> CreateOrUpdateUso(AddOrEditUsoDto model)
        {
            return Ok(await _articuloManager.CreateOrUpdateUso(model));
        }

        [HttpGet("deleteUso/{IdArticulo:int}/{item:int}")]
        public async Task<IActionResult> DeleteUsoByItem(int IdArticulo, int item)
        {
            return Ok(await _articuloManager.DeleteUsoByItem(IdArticulo, item));
        }
        [HttpGet("{id:int}/usos")]
        public async Task<IActionResult> GetUsoByIdArticulo(int id)
        {
            return Ok(await _articuloManager.GetListUsoByIdArticulo(id));
        }

        #endregion




    }
}
