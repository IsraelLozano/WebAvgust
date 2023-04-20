using IDCL.AVGUST.SIP.Manager.Articulos;
using IDCL.AVGUST.SIP.ManagerDto.Articulos;
using IDCL.AVGUST.SIP.ManagerDto.Articulos.Add;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MINEDU.IEST.Estudiante.WebApiEst.Controllers;
using System.Net.Http.Headers;

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

        [HttpGet("GetListArticulo/{IdUsuario:int}/{tipoFiltro:int}/{idIngredienteActivo:int}/{filtro?}")]
        public async Task<IActionResult> GetArticulos(int IdUsuario, int tipoFiltro, int idIngredienteActivo = 0, string filtro = "")
        {
            return Ok(await _articuloManager.GetListArticulos(IdUsuario, tipoFiltro, filtro, idIngredienteActivo));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetArticuloByID(int id)
        {
            return Ok(await _articuloManager.GetArticuloById(id));
        }
        [HttpGet("deleteArticuloById/{id:int}")]
        public async Task<IActionResult> deleteArticuloByID(int id)
        {
            return Ok(await _articuloManager.DeleteArticuloById(id));
        }

        [HttpPost("CreateOrUpdateArticulo")]
        public async Task<IActionResult> GetArticuloByID(AddOrEditArticuloDto model)
        {
            return Ok(await _articuloManager.CreateOrUpdateArticulo(model));
        }


        [HttpPost, DisableRequestSizeLimit]
        [Route("upload")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();

                //var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpGet, DisableRequestSizeLimit]
        [Route("download")]
        public IActionResult Download()
        {
            var message = "Download end-point hit!";
            return Ok(new { message });
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

        [HttpGet("{idArticulo:int}/viewPdf/{idItem:int}")]
        public async Task<IActionResult> ViewPdf(int idArticulo, int idItem)
        {
            return Ok(await _articuloManager.GetArticuloDocumentoPdf(idArticulo, idItem));
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

        [HttpGet("getetiqueta/{IdArticulo:int}")]
        public async Task<IActionResult> GetEtiqueta(int IdArticulo)
        {
            return Ok(await _articuloManager.GetEtiquetaDocumento(IdArticulo));
        }

        #endregion

        #region Producto-Fabricante

        [HttpPost("CreateOrUpdateProductoFabricante")]
        public async Task<IActionResult> CreateOrUpdateProductoFabricante(List<AddOrEditProductoFabricanteDto> model)
        {
            return Ok(await _articuloManager.CreateOrUpdateProductoFabricante(model));
        }

        [HttpGet("deleteProductoFabricante/{IdArticulo:int}/{IdFabricante:int}")]
        public async Task<IActionResult> DeleteProductoFabricanteByItem(int IdArticulo, int IdFabricante)
        {
            return Ok(await _articuloManager.DeleteProductoFabricanteById(IdArticulo, IdFabricante));
        }



        #endregion


        #region Producto-Formulador

        [HttpPost("CreateOrUpdateProductoFormulador")]
        public async Task<IActionResult> CreateOrUpdateProductoFormulador(List<AddOrEditProductoFormuladorDto> model)
        {
            return Ok(await _articuloManager.CreateOrUpdateProductoFormulador(model));
        }

        [HttpGet("deleteProductoFormulador/{IdArticulo:int}/{IdFormulador:int}")]
        public async Task<IActionResult> DeleteProductoFormuladorByItem(int IdArticulo, int IdFormulador)
        {
            return Ok(await _articuloManager.DeleteProductoFabricanteById(IdArticulo, IdFormulador));
        }



        #endregion


    }
}
