using IDCL.AVGUST.SIP.Manager.Articulos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using MINEDU.IEST.Estudiante.Inf_Utils.Dtos;
using MINEDU.IEST.Estudiante.Inf_Utils.Helpers.FileManager;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.Http.Headers;

namespace IDCL.AVGUST.SIP.WebApiEst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ResourceDto _resourceDto;
        private readonly IStorageManager _storageManager;
        private readonly IArticuloManager _articuloManager;
        public FileController(ResourceDto resourceDto, IStorageManager storageManager, IArticuloManager articuloManager)
        {
            this._resourceDto = resourceDto;
            this._storageManager = storageManager;
            _articuloManager = articuloManager;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("upload")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var pathToSave = Path.Combine(_resourceDto.UrlFileBase, _resourceDto.Documents);

                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }

                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                //var folderName = Path.Combine("Resources", "documents");

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(fileName);
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
        public async Task<IActionResult> Download([FromQuery] string fileUrl)
        {
            var filePath = Path.Combine(_resourceDto.UrlFileBase, _resourceDto.Documents, fileUrl);

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var memory = new MemoryStream();
            await using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(filePath), filePath);
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("getPhotos")]
        public IActionResult GetPhotos()
        {
            try
            {
                var folderName = Path.Combine("Resources", "Images");
                var pathToRead = Path.Combine(_resourceDto.UrlFileBase, folderName);
                var photos = Directory.EnumerateFiles(pathToRead)
                    .Where(IsAPhotoFile)
                    .Select(fullPath => Path.Combine(folderName, Path.GetFileName(fullPath)));

                return Ok(new { photos });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("GetExporToExcelArticulos/{IdUsuario:int}/{tipoFiltro:int}/{idIngredienteActivo:int}/{filtro?}")]
        public async Task<IActionResult> GetExporToExcelArticulos(int IdUsuario, int tipoFiltro, int idIngredienteActivo = 0, string filtro = "")
        {

            var articulos = await _articuloManager.GetListArticulos(IdUsuario, tipoFiltro, filtro, idIngredienteActivo);
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var xlPackage = new ExcelPackage(stream))
            {

                var worksheet = xlPackage.Workbook.Worksheets.Add("Articulos");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(Color.Blue);
                const int startRow = 5;
                var row = startRow;
                worksheet.View.ShowGridLines = false;
                //Create Headers and format them
                worksheet.Cells["A1"].Value = "Lista de Articulos";
                using (var r = worksheet.Cells["A1:G1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(Color.White);
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
                }

                worksheet.Cells["A4"].Value = "Id";
                worksheet.Cells["B4"].Value = "Nombre";
                worksheet.Cells["C4"].Value = "Pais";
                worksheet.Cells["D4"].Value = "NroRegistro";
                worksheet.Cells["E4"].Value = "TipoProducto";
                worksheet.Cells["F4"].Value = "Titular Registro";
                worksheet.Cells["G4"].Value = "Estado";
                worksheet.Cells["A4:G4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A4:G4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                worksheet.Cells["A4:G4"].Style.Font.Bold = true;

                row = 5;
                foreach (var user in articulos)
                {
                    worksheet.Cells[row, 1].Value = user.IdArticulo;
                    worksheet.Cells[row, 2].Value = user.NombreComercial;
                    worksheet.Cells[row, 3].Value = user.IdPaisNavigation.NomPais;
                    worksheet.Cells[row, 4].Value = user.NroRegistro;
                    worksheet.Cells[row, 5].Value = user.IdTipoProductoNavigation.NomTipoProducto;
                    worksheet.Cells[row, 6].Value = user.IdTitularRegistroNavigation.NomTitularRegistro;
                    worksheet.Cells[row, 7].Value = user.FlgActivo ? "ACTIVO" : "ANULADO";

                    row++;
                }

                var sRango = "A4:G" + (row - 1).ToString();
                worksheet.Cells[sRango].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells[sRango].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[sRango].AutoFitColumns();
                worksheet.Cells[sRango].Style.HorizontalAlignment = ExcelHorizontalAlignment.General;
                worksheet.Cells[sRango].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[sRango].Style.WrapText = false;

                // set some core property values
                xlPackage.Workbook.Properties.Title = "Lista de articulos";
                xlPackage.Workbook.Properties.Author = "Israel Lozano del Castillo danielitolozano85@gmail.com";
                xlPackage.Workbook.Properties.Subject = "List de Articulos";
                // save the new spreadsheet
                xlPackage.Save();
                // Response.Clear();
            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "articulos.xlsx");
        }


        private bool IsAPhotoFile(string fileName)
        {
            return fileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
                   || fileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)
                   || fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase);
        }

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;

            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}
