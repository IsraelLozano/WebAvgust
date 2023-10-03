using AutoMapper;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using IDCL.AVGUST.SIP.ManagerDto.Articulos;
using IDCL.AVGUST.SIP.ManagerDto.Reports;
using IDCL.AVGUST.SIP.Repository.UnitOfWork;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MINEDU.IEST.Estudiante.Inf_Utils.Dtos;
using MINEDU.IEST.Estudiante.Inf_Utils.Enumerados;
using MINEDU.IEST.Estudiante.Inf_Utils.Helpers.FileManager;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Org.BouncyCastle.Utilities;
using System.Drawing;

namespace IDCL.AVGUST.SIP.Manager.Reporte
{
    public class ReporteManager : IReporteManager
    {
        private readonly IMapper _mapper;
        private readonly ArticuloUnitOfWork _articuloUnitOfWork;
        private readonly MaestraUnitOfWork _maestraUnitOfWork;
        private readonly ResourceDto _resourceDto;
        private readonly IStorageManager _storageManager;
        private readonly SeguridadUnitOfWork _seguridadUnitOfWork;
        private readonly CalculatorUnitOfWork _calculatorUnitOfWork;

        public ReporteManager(IMapper mapper, ArticuloUnitOfWork articuloUnitOfWork, MaestraUnitOfWork maestraUnitOfWork, ResourceDto resourceDto, IStorageManager storageManager, SeguridadUnitOfWork seguridadUnitOfWork, CalculatorUnitOfWork calculatorUnitOfWork)
        {
            _mapper = mapper;
            _articuloUnitOfWork = articuloUnitOfWork;
            _maestraUnitOfWork = maestraUnitOfWork;
            _resourceDto = resourceDto;
            _storageManager = storageManager;
            _seguridadUnitOfWork = seguridadUnitOfWork;
            _calculatorUnitOfWork = calculatorUnitOfWork;
        }


        #region Querys
        public async Task<List<GetArticuloDto>> GetArticulosById(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo)
        {
            filtro = filtro ?? string.Empty;

            var user = _seguridadUnitOfWork._usuarioRepositoy.GetAll(p => p.IdUsuario == idUsuario, includeProperties: "UsuarioPais,UsuarioPais.IdPaisNavigation").FirstOrDefault();
            var paises = user.UsuarioPais.Select(p => p.IdPais).ToList();

            var filter = new List<Articulo>();

            if ((int)TipoBusquedaArticulo.nombre == tipoFiltro)
            {
                var query = _articuloUnitOfWork._articuloRepository.GetAll(p => paises.Contains(p.IdPais.Value) && p.FlgActivo,
              includeProperties: "IdPaisNavigation," +
           "IdTipoProductoNavigation," +
           "IdTitularRegistroNavigation," +
           "IdTipoFormulacionNavigation," +
           "Composicions.GrupoQuimicoNavegation," +
           "Composicions.IngredienteActivoNavigation," +
           //"Documentos.IdTipoDocumentoNavigation," +
           "Usos.IdCultivoNavigation," +
           "Usos.IdNomCientificoPlagaNavigation," +
           "Caracteristicas.IdClaseNavigation," +
           "Caracteristicas.IdToxicologicaNavigation," +
           "ProductoFabricantes.IdFabricanteNavigation," +
           "ProductoFormuladors.IdFormuladorNavigation",
              orderBy: p => p.OrderByDescending(l => l.IdArticulo)).AsEnumerable();

                filter = query.Where(p => filtro.Contains(p.NombreComercial, StringComparison.CurrentCultureIgnoreCase) || p.NombreComercial.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();

            }
            else if ((int)TipoBusquedaArticulo.ingredienteActivo == tipoFiltro)
            {
                var query = _articuloUnitOfWork._articuloRepository.GetAll(p => paises.Contains(p.IdPais.Value) && p.FlgActivo && p.Composicions.Any(l => l.IngredienteActivo == idIngredienteActivo),
             includeProperties: "IdPaisNavigation," +
           "IdTipoProductoNavigation," +
           "IdTitularRegistroNavigation," +
           "IdTipoFormulacionNavigation," +
           "Composicions.GrupoQuimicoNavegation," +
           "Composicions.IngredienteActivoNavigation," +
           //"Documentos.IdTipoDocumentoNavigation," +
           "Usos.IdCultivoNavigation," +
           "Usos.IdNomCientificoPlagaNavigation," +
           "Caracteristicas.IdClaseNavigation," +
           "Caracteristicas.IdToxicologicaNavigation," +
           "ProductoFabricantes.IdFabricanteNavigation," +
           "ProductoFormuladors.IdFormuladorNavigation", orderBy: p => p.OrderByDescending(l => l.IdArticulo)).AsEnumerable();
                filter = query.ToList();
            }
            else
            {
                var query = _articuloUnitOfWork._articuloRepository.GetAll(p => paises.Contains(p.IdPais.Value) && p.FlgActivo,
           includeProperties: "IdPaisNavigation," +
           "IdTipoProductoNavigation," +
           "IdTitularRegistroNavigation," +
           "IdTipoFormulacionNavigation," +
           "Composicions.GrupoQuimicoNavegation," +
           "Composicions.IngredienteActivoNavigation," +
           //"Documentos.IdTipoDocumentoNavigation," +
           "Usos.IdCultivoNavigation," +
           "Usos.IdNomCientificoPlagaNavigation," +
           "Caracteristicas.IdClaseNavigation," +
           "Caracteristicas.IdToxicologicaNavigation," +
           "ProductoFabricantes.IdFabricanteNavigation," +
           "ProductoFormuladors.IdFormuladorNavigation",
           orderBy: p => p.OrderByDescending(l => l.IdArticulo)).AsEnumerable();

                filter = query.ToList();
            }

            var response = _mapper.Map<List<GetArticuloDto>>(filter);
            return response;
        }
        public async Task<List<GetComposicionReportsDto>> GetArticulosPorComposicion(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo)
        {
            var user = _seguridadUnitOfWork._usuarioRepositoy.GetAll(p => p.IdUsuario == idUsuario, includeProperties: "UsuarioPais,UsuarioPais.IdPaisNavigation").FirstOrDefault();
            var paises = user.UsuarioPais.Select(p => p.IdPais).ToList();

            var filter = new List<Composicion>();

            if ((int)TipoBusquedaArticulo.nombre == tipoFiltro)
            {
                var query = _articuloUnitOfWork._composicionRepository.GetAll(p => paises.Contains(p.IdArticuloNavigation.IdPais.Value),
                    includeProperties: "IdArticuloNavigation.IdTitularRegistroNavigation," +
                    "IdArticuloNavigation.ProductoFormuladors.IdFormuladorNavigation," +
                    "GrupoQuimicoNavegation,IngredienteActivoNavigation",
                    orderBy: p => p.OrderByDescending(l => l.IdArticulo)).AsEnumerable();

                filter = query.Where(p => filtro.Contains(p.IdArticuloNavigation.NombreComercial, StringComparison.CurrentCultureIgnoreCase)
                || p.IdArticuloNavigation.NombreComercial.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else if ((int)TipoBusquedaArticulo.ingredienteActivo == tipoFiltro)
            {
                var query = _articuloUnitOfWork._composicionRepository.GetAll(p => paises.Contains(p.IdArticuloNavigation.IdPais.Value) && p.IngredienteActivo == idIngredienteActivo,
                  includeProperties: "IdArticuloNavigation.IdTitularRegistroNavigation," +
                  "IdArticuloNavigation.ProductoFormuladors.IdFormuladorNavigation," +
                  "GrupoQuimicoNavegation,IngredienteActivoNavigation",
                  orderBy: p => p.OrderByDescending(l => l.IdArticulo)).AsEnumerable();

                filter = query.ToList();
            }
            else
            {
                var query = _articuloUnitOfWork._composicionRepository.GetAll(p => paises.Contains(p.IdArticuloNavigation.IdPais.Value),
                    includeProperties: "IdArticuloNavigation.IdTitularRegistroNavigation," +
                    "IdArticuloNavigation.ProductoFormuladors.IdFormuladorNavigation," +
                    "GrupoQuimicoNavegation,IngredienteActivoNavigation",
                    orderBy: p => p.OrderByDescending(l => l.IdArticulo)).AsEnumerable();

                filter = query.ToList();
            }

            var response = _mapper.Map<List<GetComposicionReportsDto>>(filter);

            response.ForEach(l =>
            {
                var articulo = filter.FirstOrDefault(p => p.IdArticulo == l.IdArticulo).IdArticuloNavigation;
                if (articulo != null)
                {
                    if ((articulo.ProductoFormuladors != null && articulo.ProductoFormuladors.Any()))
                    {
                        l.listFormuladores = articulo.ProductoFormuladors.Select(s => s.IdFormuladorNavigation.NomFormulador).ToList();
                    }
                }
            });
            return response;
        }
        public async Task<List<GetPlagaReportsDto>> GetArticulosPorPlaga(int idUsuario, string filtro)
        {
            filtro = filtro ?? string.Empty;

            var user = _seguridadUnitOfWork._usuarioRepositoy.GetAll(p => p.IdUsuario == idUsuario, includeProperties: "UsuarioPais,UsuarioPais.IdPaisNavigation").FirstOrDefault();
            var paises = user.UsuarioPais.Select(p => p.IdPais).ToList();

            var filter = new List<Uso>();

            var query = _articuloUnitOfWork._usoRepository.GetAll(p => paises.Contains(p.IdArticuloNavigation.IdPais.Value),
                includeProperties: "IdArticuloNavigation.IdPaisNavigation," +
                "IdArticuloNavigation.IdTitularRegistroNavigation," +
                "IdArticuloNavigation.IdPaisNavigation," +
                "IdCultivoNavigation," +
                "IdNomCientificoPlagaNavigation",
                orderBy: p => p.OrderBy(l => l.IdNomCientificoPlagaNavigation.NombreCientificoPlaga)).ToList();

            //var query = _articuloUnitOfWork._articuloRepository.GetAll(p => paises.Contains(p.IdPais.Value)
            //&& p.FlgActivo,
            //    includeProperties: "IdPaisNavigation,IdTitularRegistroNavigation,Usos,Usos.IdCultivoNavigation,Usos.IdNomCientificoPlagaNavigation",
            //    orderBy: p => p.OrderByDescending(l => l.NombreComercial)).ToList();

            if (string.IsNullOrEmpty(filtro))
            {
                filter = query.ToList();
            }
            else
            {
                filter = query.Where(p => p.IdNomCientificoPlagaNavigation.NombreCientificoPlaga.Contains(filtro)).ToList();
            }

            var response = _mapper.Map<List<GetPlagaReportsDto>>(filter);
            return response;
        }
        public async Task<List<GetCultivoReportsDto>> GetArticulosPorCultivo(int idUsuario, string filtro)
        {
            var user = _seguridadUnitOfWork._usuarioRepositoy.GetAll(p => p.IdUsuario == idUsuario, includeProperties: "UsuarioPais,UsuarioPais.IdPaisNavigation").FirstOrDefault();
            var paises = user.UsuarioPais.Select(p => p.IdPais).ToList();

            filtro = filtro ?? string.Empty;

            var filter = new List<Uso>();

            var query = _articuloUnitOfWork._usoRepository.GetAll(p => paises.Contains(p.IdArticuloNavigation.IdPais.Value),
                 includeProperties: "IdArticuloNavigation.IdPaisNavigation," +
                 "IdArticuloNavigation.IdTitularRegistroNavigation," +
                 "IdArticuloNavigation.IdPaisNavigation," +
                 "IdCultivoNavigation," +
                 "IdNomCientificoPlagaNavigation",
                 orderBy: p => p.OrderBy(l => l.IdCultivoNavigation.NombreCultivo)).ToList();


            if (string.IsNullOrEmpty(filtro))
            {
                filter = query.ToList();
            }
            else
            {
                filter = query.Where(p => p.IdCultivoNavigation.NombreCultivo.Contains(filtro)).ToList();
            }
            //filter = query.Where(p => filtro.Contains(p.NombreComercial, StringComparison.CurrentCultureIgnoreCase) || p.NombreComercial.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();


            var response = _mapper.Map<List<GetCultivoReportsDto>>(filter);
            return response;
        }
        public async Task<List<GetProductoFabricanteReportsDto>> GetArticulosFabricante(int idUsuario, string filtro)
        {
            var user = _seguridadUnitOfWork._usuarioRepositoy.GetAll(p => p.IdUsuario == idUsuario, includeProperties: "UsuarioPais,UsuarioPais.IdPaisNavigation").FirstOrDefault();
            var paises = user.UsuarioPais.Select(p => p.IdPais).ToList();

            filtro = filtro ?? string.Empty;

            var filter = new List<ProductoFabricante>();

            var query = _articuloUnitOfWork._productoFabricanteRepository.GetAll(p => paises.Contains(p.IdArticuloNavigation.IdPais.Value) && p.IdArticuloNavigation.FlgActivo,
                includeProperties: "IdArticuloNavigation.IdPaisNavigation," +
                "IdArticuloNavigation.IdTitularRegistroNavigation," +
                "IdFabricanteNavigation," +
                "IdArticuloNavigation.Composicions.IngredienteActivoNavigation",
                orderBy: p => p.OrderBy(l => l.IdFabricanteNavigation.NombreFabricante)).AsEnumerable();

            //var query = _articuloUnitOfWork._articuloRepository.GetAll(p => paises.Contains(p.IdPais.Value) && p.FlgActivo,
            //    includeProperties: "IdPaisNavigation,IdTitularRegistroNavigation,ProductoFabricantes.IdFabricanteNavigation,Composicions.IngredienteActivoNavigation",
            //    orderBy: p => p.OrderByDescending(l => l.IdArticulo)).AsEnumerable();

            if (string.IsNullOrEmpty(filtro))
            {
                filter = query.ToList();
            }
            else
            {
                filter = query.Where(p => p.IdFabricanteNavigation.NombreFabricante.Contains(filtro)).ToList();

            }

            //filter = query.Where(p => filtro.Contains(p.NombreComercial, StringComparison.CurrentCultureIgnoreCase) || p.NombreComercial.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();

            var response = _mapper.Map<List<GetProductoFabricanteReportsDto>>(filter.ToList());

            response.ForEach(l =>
            {
                var articulo = filter.FirstOrDefault(p => p.IdArticulo == l.IdArticulo);
                if (articulo != null)
                {
                    if ((articulo.IdArticuloNavigation.Composicions != null && articulo.IdArticuloNavigation.Composicions.Any()))
                    {
                        l.IdArticuloNavigation.ingredientesActivos = articulo.IdArticuloNavigation.Composicions.Select(s => s.IngredienteActivoNavigation.NomIngredienteActivo).ToList();
                    }
                }
            });

            return response;
        }
        public async Task<List<GetProductoFormuladorReportsDto>> GetArticulosFormuladorAll(int idUsuario, string filtro)
        {
            var user = _seguridadUnitOfWork._usuarioRepositoy.GetAll(p => p.IdUsuario == idUsuario, includeProperties: "UsuarioPais,UsuarioPais.IdPaisNavigation").FirstOrDefault();
            var paises = user.UsuarioPais.Select(p => p.IdPais).ToList();

            filtro = filtro ?? string.Empty;

            var filter = new List<ProductoFormulador>();

            var query = _articuloUnitOfWork._productoFormuladorRepository.GetAll(p => paises.Contains(p.IdProductoNavigation.IdPais.Value) && p.IdProductoNavigation.FlgActivo,
              includeProperties: "IdProductoNavigation.IdPaisNavigation," +
              "IdProductoNavigation.IdTitularRegistroNavigation," +
              "IdFormuladorNavigation," +
              "IdProductoNavigation.Composicions.IngredienteActivoNavigation",
              orderBy: p => p.OrderBy(l => l.IdFormuladorNavigation.NomFormulador)).AsEnumerable();

            if (string.IsNullOrEmpty(filtro))
            {
                filter = query.ToList();
            }
            else
            {
                filter = query.Where(p => p.IdFormuladorNavigation.NomFormulador.Contains(filtro)).ToList();
            }

            var response = _mapper.Map<List<GetProductoFormuladorReportsDto>>(filter.ToList());

            response.ForEach(l =>
            {
                var articulo = filter.FirstOrDefault(p => p.IdProducto == l.IdProducto);
                if (articulo != null)
                {
                    if ((articulo.IdProductoNavigation.Composicions != null && articulo.IdProductoNavigation.Composicions.Any()))
                    {
                        l.IdProductoNavigation.ingredientesActivos = articulo.IdProductoNavigation.Composicions.Select(s => s.IngredienteActivoNavigation.NomIngredienteActivo).ToList();
                    }
                }
            });

            return response;
        }

        #endregion

        #region Reporte Excel

        public async Task<MemoryStream> GetExcelArticulosGeneral(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo)
        {
            var data = await this.GetArticulosById(idUsuario, tipoFiltro, filtro, idIngredienteActivo);
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("articulos");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                const int startRow = 5;
                var row = startRow;
                worksheet.View.ShowGridLines = false;
                //Create Headers and format them
                worksheet.Cells["A1"].Value = "REPORTE DE PRODUCTOS FORMULADOS";
                using (var r = worksheet.Cells["A1:O1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Size = 16;
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(23, 55, 93));
                }


                worksheet.Cells["A4"].Value = "Nombre Comercial";
                worksheet.Cells["B4"].Value = "Nro Registro";
                worksheet.Cells["C4"].Value = "Pais";
                worksheet.Cells["D4"].Value = "Titular Registro";
                worksheet.Cells["E4"].Value = "Tipo Producto";
                worksheet.Cells["F4"].Value = "Tipo Formulacion";
                worksheet.Cells["G4"].Value = "Ingrediente Activo";
                worksheet.Cells["H4"].Value = "Concentracion (IA)";
                worksheet.Cells["I4"].Value = "Toxicologia";
                worksheet.Cells["J4"].Value = "Cultivo";
                worksheet.Cells["K4"].Value = "Plaga";
                worksheet.Cells["L4"].Value = "Dosis";
                worksheet.Cells["M4"].Value = "Formuladores";
                worksheet.Cells["N4"].Value = "Fabricantes";
                worksheet.Cells["A4:N4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A4:N4"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(184, 204, 228));
                worksheet.Cells["A4:N4"].Style.Font.Bold = true;


                row = 5;
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.NombreComercial;
                    //worksheet.Cells[row, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[row, 2].Value = item.NroRegistro;
                    worksheet.Cells[row, 3].Value = item.IdPaisNavigation.NomPais;
                    worksheet.Cells[row, 4].Value = item.IdTitularRegistroNavigation.NomTitularRegistro;
                    worksheet.Cells[row, 5].Value = item.IdTipoProductoNavigation.NomTipoProducto;
                    worksheet.Cells[row, 6].Value = item.IdTipoFormulacionNavigation.NomTipoFormulacion;
                    //worksheet.Cells[row, 7].Value = item.NomFormulador;
                    worksheet.Cells[row, 8].Value = "'" + string.Join(Environment.NewLine, item.Composicions.Select(p => $"- {p.IngredienteActivoNavigation.NomIngredienteActivo}"));
                    worksheet.Cells[row, 9].Value = "'" + string.Join(Environment.NewLine, item.Composicions.Select(p => $"- {p.ContracionIA}"));
                    worksheet.Cells[row, 10].Value = "'" + string.Join(Environment.NewLine, item.Caracteristicas.Select(p => $"- {p.IdToxicologicaNavigation.Descripcion}"));
                    worksheet.Cells[row, 11].Value = "'" + string.Join(Environment.NewLine, item.Usos.Select(p => $"- {p.IdCultivoNavigation.NombreCultivo}").Distinct());
                    worksheet.Cells[row, 12].Value = "'" + string.Join(Environment.NewLine, item.Usos.Select(p => $"- {p.IdNomCientificoPlagaNavigation.NombreCientificoPlaga}").Distinct());
                    worksheet.Cells[row, 13].Value = "Ver Etiqueta";
                    worksheet.Cells[row, 14].Value = "'" + string.Join(Environment.NewLine, item.ProductoFormuladors.Select(p => $"- {p.IdFormuladorNavigation.NomFormulador}"));
                    worksheet.Cells[row, 15].Value = "'" + string.Join(Environment.NewLine, item.ProductoFabricantes.Select(p => $"- {p.IdFabricanteNavigation.NombreFabricante}"));

                    row++;
                }
                var sRango = "A4:N" + (row - 1).ToString();
                worksheet.Cells[sRango].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells[sRango].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[sRango].AutoFitColumns();
                worksheet.Cells[sRango].Style.HorizontalAlignment = ExcelHorizontalAlignment.General;
                worksheet.Cells[sRango].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[sRango].Style.WrapText = true;

                xlPackage.Workbook.Properties.Title = "Lista de articulos";
                xlPackage.Workbook.Properties.Author = "Israel Lozano del Castillo danielitolozano85@gmail.com";
                xlPackage.Workbook.Properties.Subject = "List de Articulos";
                xlPackage.Save();
                // Response.Clear();

            }
            stream.Position = 0;

            return stream;
        }
        public async Task<MemoryStream> GetExcelArticulosPorComposicion(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo)
        {
            var data = await this.GetArticulosPorComposicion(idUsuario, tipoFiltro, filtro, idIngredienteActivo);
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("articulos");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                const int startRow = 5;
                var row = startRow;
                worksheet.View.ShowGridLines = false;

                //Create Headers and format them
                worksheet.Cells["A1"].Value = "REPORTE POR COMPOSICIÓN";
                using (var r = worksheet.Cells["A1:F1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(23, 55, 93));
                }

                worksheet.Cells["A4"].Value = "Nombre Comercial";
                worksheet.Cells["B4"].Value = "Ingrediente Activo";
                worksheet.Cells["C4"].Value = "Concentracion (IA)";
                worksheet.Cells["D4"].Value = "Grupo Quimico";
                worksheet.Cells["E4"].Value = "Titular Registro";
                worksheet.Cells["F4"].Value = "Formuladores";
                //worksheet.Cells["F4"].Value = "Formulador";
                worksheet.Cells["A4:F4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A4:F4"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(184, 204, 228));
                worksheet.Cells["A4:F4"].Style.Font.Bold = true;


                row = 5;
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.IdArticuloNavigation.NombreComercial;
                    worksheet.Cells[row, 2].Value = item.IngredienteActivoNavigation.NomIngredienteActivo;
                    worksheet.Cells[row, 3].Value = item.ContracionIA;
                    worksheet.Cells[row, 4].Value = item.GrupoQuimicoNavegation.NomGrupoQuimico;
                    worksheet.Cells[row, 5].Value = item.IdArticuloNavigation.IdTitularRegistroNavigation != null
                        ? item.IdArticuloNavigation.IdTitularRegistroNavigation.NomTitularRegistro
                        : string.Empty;

                    worksheet.Cells[row, 6].Value = (item.listFormuladores != null && item.listFormuladores.Any()) ? "'" + string.Join(Environment.NewLine, item.listFormuladores.Select(p => $"- {p}")) : string.Empty;
                    row++;
                }

                var sRango = "A4:F" + (row - 1).ToString();
                worksheet.Cells[sRango].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells[sRango].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[sRango].AutoFitColumns();
                worksheet.Cells[sRango].Style.HorizontalAlignment = ExcelHorizontalAlignment.General;
                worksheet.Cells[sRango].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[sRango].Style.WrapText = true;

                xlPackage.Workbook.Properties.Title = "Lista de articulos";
                xlPackage.Workbook.Properties.Author = "Israel Lozano del Castillo danielitolozano85@gmail.com";
                xlPackage.Workbook.Properties.Subject = "List de Articulos";
                xlPackage.Save();
                // Response.Clear();

            }
            stream.Position = 0;

            return stream;
        }
        public async Task<MemoryStream> GetExcelArticulosPorPlaga(int idUsuario, string filtro)
        {
            var data = await this.GetArticulosPorPlaga(idUsuario, filtro);
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("articulos");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                const int startRow = 5;
                var row = startRow;
                worksheet.View.ShowGridLines = false;

                //Create Headers and format them
                worksheet.Cells["A1"].Value = "REPORTE POR PLAGA";
                using (var r = worksheet.Cells["A1:F1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(23, 55, 93));
                }

                worksheet.Cells["A4"].Value = "Plaga";
                worksheet.Cells["B4"].Value = "Nombre Comercial";
                worksheet.Cells["C4"].Value = "Cultivo";
                worksheet.Cells["D4"].Value = "Dosis";
                worksheet.Cells["E4"].Value = "Titular Registro";
                worksheet.Cells["F4"].Value = "Pais";
                worksheet.Cells["A4:F4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A4:F4"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(184, 204, 228));
                worksheet.Cells["A4:F4"].Style.Font.Bold = true;


                row = 5;
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.IdNomCientificoPlagaNavigation.NombreCientificoPlaga;
                    worksheet.Cells[row, 2].Value = item.IdArticuloNavigation.NombreComercial;
                    worksheet.Cells[row, 3].Value = item.IdCultivoNavigation.NombreCultivo;
                    worksheet.Cells[row, 4].Value = "Ver Etiqueta";
                    worksheet.Cells[row, 5].Value = item.IdArticuloNavigation.IdTitularRegistroNavigation.NomTitularRegistro;
                    worksheet.Cells[row, 6].Value = item.IdArticuloNavigation.IdPaisNavigation.NomPais;
                    row++;
                }

                var sRango = "A4:F" + (row - 1).ToString();
                worksheet.Cells[sRango].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells[sRango].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[sRango].AutoFitColumns();
                worksheet.Cells[sRango].Style.HorizontalAlignment = ExcelHorizontalAlignment.General;
                worksheet.Cells[sRango].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[sRango].Style.WrapText = true;

                xlPackage.Workbook.Properties.Title = "Lista de articulos";
                xlPackage.Workbook.Properties.Author = "Israel Lozano del Castillo danielitolozano85@gmail.com";
                xlPackage.Workbook.Properties.Subject = "List de Articulos";
                xlPackage.Save();
                // Response.Clear();

            }
            stream.Position = 0;

            return stream;
        }
        public async Task<MemoryStream> GetExcelArticulosPorCultivo(int idUsuario, string filtro)
        {
            var data = await this.GetArticulosPorCultivo(idUsuario, filtro);
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("articulos");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                const int startRow = 5;
                var row = startRow;
                worksheet.View.ShowGridLines = false;

                //Create Headers and format them
                worksheet.Cells["A1"].Value = "REPORTE POR CULTIVO";
                using (var r = worksheet.Cells["A1:F1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(23, 55, 93));
                }

                worksheet.Cells["A4"].Value = "Cultivo";
                worksheet.Cells["B4"].Value = "Nombre Comercial";
                worksheet.Cells["C4"].Value = "Plaga";
                worksheet.Cells["D4"].Value = "Dosis";
                worksheet.Cells["E4"].Value = "Titular Registro";
                worksheet.Cells["F4"].Value = "Pais";
                worksheet.Cells["A4:F4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A4:F4"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(184, 204, 228));
                worksheet.Cells["A4:F4"].Style.Font.Bold = true;


                row = 5;
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.IdCultivoNavigation.NombreCultivo;
                    worksheet.Cells[row, 2].Value = item.IdArticuloNavigation.NombreComercial;
                    worksheet.Cells[row, 3].Value = item.IdNomCientificoPlagaNavigation.NombreCientificoPlaga;
                    worksheet.Cells[row, 4].Value = "Ver Etiqueta";
                    worksheet.Cells[row, 5].Value = item.IdArticuloNavigation.IdTitularRegistroNavigation.NomTitularRegistro;
                    worksheet.Cells[row, 6].Value = item.IdArticuloNavigation.IdPaisNavigation.NomPais;
                    row++;
                }

                var sRango = "A4:F" + (row - 1).ToString();
                worksheet.Cells[sRango].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells[sRango].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[sRango].AutoFitColumns();
                worksheet.Cells[sRango].Style.HorizontalAlignment = ExcelHorizontalAlignment.General;
                worksheet.Cells[sRango].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[sRango].Style.WrapText = true;

                xlPackage.Workbook.Properties.Title = "Lista de articulos";
                xlPackage.Workbook.Properties.Author = "Israel Lozano del Castillo danielitolozano85@gmail.com";
                xlPackage.Workbook.Properties.Subject = "List de Articulos";
                xlPackage.Save();
                // Response.Clear();

            }
            stream.Position = 0;

            return stream;
        }
        public async Task<MemoryStream> GetExcelGetArticulosFabricante(int idUsuario, string filtro)
        {
            var data = await this.GetArticulosFabricante(idUsuario, filtro);
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("articulos");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                const int startRow = 5;
                var row = startRow;
                worksheet.View.ShowGridLines = false;

                //Create Headers and format them
                worksheet.Cells["A1"].Value = "REPORTE POR FABRICANTE";
                using (var r = worksheet.Cells["A1:D1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(23, 55, 93));
                }

                worksheet.Cells["A4"].Value = "Fabricante";
                worksheet.Cells["B4"].Value = "Nombre Comercial";
                worksheet.Cells["C4"].Value = "Titular Registro";
                worksheet.Cells["D4"].Value = "Ingrediente Activo";
                worksheet.Cells["A4:D4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A4:D4"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(184, 204, 228));
                worksheet.Cells["A4:D4"].Style.Font.Bold = true;


                row = 5;
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.IdFabricanteNavigation.NombreFabricante;
                    worksheet.Cells[row, 2].Value = item.IdArticuloNavigation.NombreComercial;
                    worksheet.Cells[row, 3].Value = item.IdArticuloNavigation.IdTitularRegistroNavigation.NomTitularRegistro;

                    worksheet.Cells[row, 4].Value = item.IdArticuloNavigation.ingredientesActivos != null && item.IdArticuloNavigation.ingredientesActivos.Any() ? "'" + string.Join(Environment.NewLine, item.IdArticuloNavigation.ingredientesActivos.Select(p => $"- {p}")) : string.Empty;
                    row++;
                }

                var sRango = "A4:D" + (row - 1).ToString();
                worksheet.Cells[sRango].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells[sRango].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[sRango].AutoFitColumns();
                worksheet.Cells[sRango].Style.HorizontalAlignment = ExcelHorizontalAlignment.General;
                worksheet.Cells[sRango].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[sRango].Style.WrapText = true;

                xlPackage.Workbook.Properties.Title = "Lista de articulos";
                xlPackage.Workbook.Properties.Author = "Israel Lozano del Castillo danielitolozano85@gmail.com";
                xlPackage.Workbook.Properties.Subject = "List de Articulos";
                xlPackage.Save();
                // Response.Clear();

            }
            stream.Position = 0;

            return stream;
        }
        public async Task<MemoryStream> GetExcelGetArticulosFormuladorAll(int idUsuario, string filtro)
        {
            var data = await this.GetArticulosFormuladorAll(idUsuario, filtro);
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("articulos");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                const int startRow = 5;
                var row = startRow;
                worksheet.View.ShowGridLines = false;

                //Create Headers and format them
                worksheet.Cells["A1"].Value = "REPORTE POR FOMULADORES";
                using (var r = worksheet.Cells["A1:D1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(23, 55, 93));
                }

                worksheet.Cells["A4"].Value = "Formulador";
                worksheet.Cells["B4"].Value = "Nombre Comercial";
                worksheet.Cells["C4"].Value = "Titular Registro";
                worksheet.Cells["D4"].Value = "Ingrediente Activo";
                worksheet.Cells["A4:D4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A4:D4"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(184, 204, 228));
                worksheet.Cells["A4:D4"].Style.Font.Bold = true;


                row = 5;
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.IdFormuladorNavigation.NomFormulador;
                    worksheet.Cells[row, 2].Value = item.IdProductoNavigation.NombreComercial;
                    worksheet.Cells[row, 3].Value = item.IdProductoNavigation.IdTitularRegistroNavigation.NomTitularRegistro;

                    worksheet.Cells[row, 4].Value = item.IdProductoNavigation.ingredientesActivos != null && item.IdProductoNavigation.ingredientesActivos.Any() ? "'" + string.Join(Environment.NewLine, item.IdProductoNavigation.ingredientesActivos.Select(p => $"- {p}")) : string.Empty;
                    row++;
                }

                var sRango = "A4:D" + (row - 1).ToString();
                worksheet.Cells[sRango].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells[sRango].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[sRango].AutoFitColumns();
                worksheet.Cells[sRango].Style.HorizontalAlignment = ExcelHorizontalAlignment.General;
                worksheet.Cells[sRango].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[sRango].Style.WrapText = true;

                xlPackage.Workbook.Properties.Title = "Lista de articulos";
                xlPackage.Workbook.Properties.Author = "Israel Lozano del Castillo danielitolozano85@gmail.com";
                xlPackage.Workbook.Properties.Subject = "List de Articulos";
                xlPackage.Save();
                // Response.Clear();

            }
            stream.Position = 0;

            return stream;
        }
        #endregion

        #region Reportes - PDF
        public async Task<GetPdfDto> GetProductosFormuladosPdfAsync(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo)
        {
            var model = await GetArticulosById(idUsuario, tipoFiltro, filtro, idIngredienteActivo);

            var fileLogo = System.IO.Path.Combine(_resourceDto.Documents, "images", "logo-avgust.jpg");

            PdfFont fontSubTitle = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontTitle = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontHeaderTable = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontTable = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontTexto = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontMR = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontFirma = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            float setHeight = 13f;
            float setFontSize = 7f;
            float setMinHeight = 6f;

            PdfWriter writer = null;

            var cellColor = new DeviceRgb(224, 224, 224);

            MemoryStream baos = new MemoryStream();

            using (writer = new PdfWriter(baos))
            {
                PdfDocument pdf = new PdfDocument(writer);
                Document doc = new Document(pdf, PageSize.A4.Rotate(), false);

                #region LOGO

                iText.Layout.Element.Image image = new iText.Layout.Element.Image(ImageDataFactory.Create(fileLogo));
                image.GetXObject().GetPdfObject().SetCompressionLevel(CompressionConstants.BEST_COMPRESSION);

                image.ScaleToFit(200, 35);
                float offsetX = (200 - image.GetImageScaledWidth());
                float offsetY = (523 - image.GetImageScaledHeight());
                image.SetFixedPosition(9 + offsetX, 60 + offsetY);

                //image.ScaleAbsolute(200, 40);
                //image.SetFixedPosition(PageSize.A4.Rotate().GetWidth() / 2, 800);

                doc.Add(image);
                doc.Add(new Paragraph(" "));

                #endregion


                #region Titulo de Pagina
                Paragraph title = new Paragraph();
                title.SetFont(fontTitle).SetFontSize(setFontSize).SetFontColor(ColorConstants.BLACK).SetBold()
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetUnderline()
                    .Add("SISTEMA DE INFORMACIÓN DE PLAGUICIDA\r\nReporte de Productos Formulados");

                doc.Add(title);

                #endregion


                #region Data

                Table tableBienesServicios = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 })).UseAllAvailableWidth().SetVerticalBorderSpacing(3).SetHorizontalBorderSpacing(3);
                //SetBackgroundColor(cellColor)
                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Item")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Nombre Comercial")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Nro Registro")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Pais")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Titular Registro")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Tipo Producto")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Tipo Formulacion")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                //tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Formulador")
                //    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Ingrediente Activo")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Concentracion (IA)")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Toxicologia")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Cultivo")
               .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Plaga")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Formuladores")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Fabricantes")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                int ind = 0;

                foreach (var item in model)
                {
                    ind++;

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(ind.ToString())
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.NombreComercial)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.NroRegistro)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.IdPaisNavigation.NomPais)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.IdTitularRegistroNavigation.NomTitularRegistro)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.IdTipoProductoNavigation.NomTipoProducto)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.IdTipoFormulacionNavigation.NomTipoFormulacion)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(string.Join(Environment.NewLine, item.Composicions.Select(p => $"- {p.IngredienteActivoNavigation.NomIngredienteActivo}")))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(string.Join(Environment.NewLine, item.Composicions.Select(p => $"- {p.ContracionIA}")))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(string.Join(Environment.NewLine, item.Caracteristicas.Select(p => $"- {p.IdToxicologicaNavigation.Descripcion}")))
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(string.Join(Environment.NewLine, item.Usos.Select(p => $"- {p.IdCultivoNavigation.NombreCultivo}").Distinct()))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                                       .Add(new Paragraph(string.Join(Environment.NewLine, item.Usos.Select(p => $"- {p.IdNomCientificoPlagaNavigation.NombreCientificoPlaga}").Distinct()))
                                       .SetTextAlignment(TextAlignment.LEFT)
                                       .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                                       .Add(new Paragraph(string.Join(Environment.NewLine, item.ProductoFormuladors.Select(p => $"- {p.IdFormuladorNavigation.NomFormulador}")))
                                       .SetTextAlignment(TextAlignment.LEFT)
                                       .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                                       .Add(new Paragraph(string.Join(Environment.NewLine, item.ProductoFabricantes.Select(p => $"- {p.IdFabricanteNavigation.NombreFabricante}")))
                                       .SetTextAlignment(TextAlignment.LEFT)
                                       .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));
                }

                doc.Add(tableBienesServicios);
                #endregion


                #region Numeración de Página
                int numberOfPages = pdf.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {
                    doc.ShowTextAligned(new Paragraph($"Pag: {i} / {numberOfPages}"),
                        800, 590, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0).SetFontSize(setFontSize - 1);

                    doc.ShowTextAligned(new Paragraph($"Fecha y Hora de Emisión: {DateTime.Now.ToString("dd/MM/yyyy hh:mm tt")}"),
                       780, 575, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0).SetFontSize(setFontSize - 1);
                }
                #endregion

                // Ceramos el documento
                doc.Close();
            }

            var pdf64 = Convert.ToBase64String(baos.ToArray());
            GetPdfDto data = new GetPdfDto
            {
                base64 = pdf64
            };
            return data;

        }
        public async Task<GetPdfDto> GetArticulosPorComposicionPdfAsync(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo)
        {
            var model = await GetArticulosPorComposicion(idUsuario, tipoFiltro, filtro, idIngredienteActivo);

            var fileLogo = System.IO.Path.Combine(_resourceDto.Documents, "images", "logo-avgust.jpg");

            PdfFont fontSubTitle = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontTitle = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontHeaderTable = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontTable = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontTexto = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontMR = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontFirma = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            float setHeight = 13f;
            float setFontSize = 7f;
            float setMinHeight = 6f;

            PdfWriter writer = null;

            var cellColor = new DeviceRgb(224, 224, 224);

            MemoryStream baos = new MemoryStream();

            using (writer = new PdfWriter(baos))
            {
                PdfDocument pdf = new PdfDocument(writer);
                Document doc = new Document(pdf, PageSize.A4.Rotate(), false);

                #region LOGO

                iText.Layout.Element.Image image = new iText.Layout.Element.Image(ImageDataFactory.Create(fileLogo));
                image.GetXObject().GetPdfObject().SetCompressionLevel(CompressionConstants.BEST_COMPRESSION);

                image.ScaleToFit(200, 35);
                float offsetX = (200 - image.GetImageScaledWidth());
                float offsetY = (523 - image.GetImageScaledHeight());
                image.SetFixedPosition(9 + offsetX, 60 + offsetY);

                //image.ScaleAbsolute(200, 40);
                //image.SetFixedPosition(PageSize.A4.Rotate().GetWidth() / 2, 800);

                doc.Add(image);
                doc.Add(new Paragraph(" "));

                #endregion

                #region Titulo de Pagina
                Paragraph title = new Paragraph();
                title.SetFont(fontTitle).SetFontSize(setFontSize).SetFontColor(ColorConstants.BLACK).SetBold()
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetUnderline()
                    .Add("SISTEMA DE INFORMACIÓN DE PLAGUICIDA\r\nReporte de Composición");

                doc.Add(title);

                #endregion

                #region Data

                Table tableBienesServicios = new Table(UnitValue.CreatePercentArray(new float[] { 0.2f, 2, 2, 2, 2, 2, 2 })).UseAllAvailableWidth().SetVerticalBorderSpacing(3).SetHorizontalBorderSpacing(3);

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Item")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Ingrediente Activo")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Nombre Comercial")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Titular Registro")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Concentracion IA")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Grupo Químico")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Formuladores")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));


                int ind = 0;

                foreach (var item in model)
                {
                    ind++;

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(ind.ToString())
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.IngredienteActivoNavigation.NomIngredienteActivo))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.IdArticuloNavigation.NombreComercial)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.IdArticuloNavigation.IdTitularRegistroNavigation != null ? item.IdArticuloNavigation.IdTitularRegistroNavigation.NomTitularRegistro : string.Empty)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.ContracionIA)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.GrupoQuimicoNavegation.NomGrupoQuimico)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph((item.listFormuladores != null && item.listFormuladores.Any()) ? string.Join(Environment.NewLine, item.listFormuladores.Select(p => $"- {p}")) : string.Empty)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));
                }

                doc.Add(tableBienesServicios);
                #endregion

                #region Numeración de Página
                int numberOfPages = pdf.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {
                    doc.ShowTextAligned(new Paragraph($"Pag: {i} / {numberOfPages}"),
                        800, 590, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0).SetFontSize(setFontSize - 1);

                    doc.ShowTextAligned(new Paragraph($"Fecha y Hora de Emisión: {DateTime.Now.ToString("dd/MM/yyyy hh:mm tt")}"),
                       780, 575, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0).SetFontSize(setFontSize - 1);
                }
                #endregion

                // Ceramos el documento
                doc.Close();
            }

            var pdf64 = Convert.ToBase64String(baos.ToArray());
            GetPdfDto data = new GetPdfDto
            {
                base64 = pdf64
            };
            return data;

        }
        public async Task<GetPdfDto> GetArticulosPorPlagaPdfAsync(int idUsuario, string filtro)
        {
            var model = await GetArticulosPorPlaga(idUsuario, filtro);

            var fileLogo = System.IO.Path.Combine(_resourceDto.Documents, "images", "logo-avgust.jpg");

            PdfFont fontSubTitle = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontTitle = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontHeaderTable = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontTable = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontTexto = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontMR = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontFirma = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            float setHeight = 13f;
            float setFontSize = 7f;
            float setMinHeight = 6f;

            PdfWriter writer = null;

            var cellColor = new DeviceRgb(224, 224, 224);

            MemoryStream baos = new MemoryStream();

            using (writer = new PdfWriter(baos))
            {
                PdfDocument pdf = new PdfDocument(writer);
                Document doc = new Document(pdf, PageSize.A4.Rotate(), false);

                #region LOGO

                iText.Layout.Element.Image image = new iText.Layout.Element.Image(ImageDataFactory.Create(fileLogo));
                image.GetXObject().GetPdfObject().SetCompressionLevel(CompressionConstants.BEST_COMPRESSION);

                image.ScaleToFit(200, 35);
                float offsetX = (200 - image.GetImageScaledWidth());
                float offsetY = (523 - image.GetImageScaledHeight());
                image.SetFixedPosition(9 + offsetX, 60 + offsetY);

                //image.ScaleAbsolute(200, 40);
                //image.SetFixedPosition(PageSize.A4.Rotate().GetWidth() / 2, 800);

                doc.Add(image);
                doc.Add(new Paragraph(" "));

                #endregion


                #region Titulo de Pagina
                Paragraph title = new Paragraph();
                title.SetFont(fontTitle).SetFontSize(setFontSize).SetFontColor(ColorConstants.BLACK).SetBold()
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetUnderline()
                    .Add("SISTEMA DE INFORMACIÓN DE PLAGUICIDA\r\nReporte de Plagas");

                doc.Add(title);

                #endregion


                #region Data

                Table tableBienesServicios = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2, 2, 2, 2, 2 })).UseAllAvailableWidth().SetVerticalBorderSpacing(3).SetHorizontalBorderSpacing(3);

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Item")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Plaga")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Nombre Comercial")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Pais")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Titular Registro")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Cultivo")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));


                int ind = 0;

                foreach (var item in model)
                {
                    ind++;

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                 .Add(new Paragraph(ind.ToString())
                 .SetTextAlignment(TextAlignment.LEFT)
                 .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.IdNomCientificoPlagaNavigation.NombreCientificoPlaga)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(fontTable)
                        .SetFontSize(setFontSize - 1)
                        .SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.IdArticuloNavigation.NombreComercial)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.IdArticuloNavigation.IdPaisNavigation.NomPais)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                                       .Add(new Paragraph(item.IdArticuloNavigation.IdTitularRegistroNavigation.NomTitularRegistro)
                                       .SetTextAlignment(TextAlignment.LEFT)
                                       .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                                       .Add(new Paragraph(item.IdCultivoNavigation.NombreCultivo)
                                       .SetTextAlignment(TextAlignment.LEFT)
                                       .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));


                }

                doc.Add(tableBienesServicios);
                #endregion


                #region Numeración de Página
                int numberOfPages = pdf.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {
                    doc.ShowTextAligned(new Paragraph($"Pag: {i} / {numberOfPages}"),
                        800, 590, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0).SetFontSize(setFontSize - 1);

                    doc.ShowTextAligned(new Paragraph($"Fecha y Hora de Emisión: {DateTime.Now.ToString("dd/MM/yyyy hh:mm tt")}"),
                       780, 575, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0).SetFontSize(setFontSize - 1);
                }
                #endregion

                // Ceramos el documento
                doc.Close();
            }

            var pdf64 = Convert.ToBase64String(baos.ToArray());
            GetPdfDto data = new GetPdfDto
            {
                base64 = pdf64
            };
            return data;

        }
        public async Task<GetPdfDto> GetArticulosPorCultivoPdfAsync(int idUsuario, string filtro)
        {
            var model = await GetArticulosPorCultivo(idUsuario, filtro);

            var fileLogo = System.IO.Path.Combine(_resourceDto.Documents, "images", "logo-avgust.jpg");

            PdfFont fontSubTitle = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontTitle = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontHeaderTable = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontTable = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontTexto = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontMR = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontFirma = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            float setHeight = 13f;
            float setFontSize = 7f;
            float setMinHeight = 6f;

            PdfWriter writer = null;

            var cellColor = new DeviceRgb(224, 224, 224);

            MemoryStream baos = new MemoryStream();

            using (writer = new PdfWriter(baos))
            {
                PdfDocument pdf = new PdfDocument(writer);
                Document doc = new Document(pdf, PageSize.A4.Rotate(), false);

                #region LOGO

                iText.Layout.Element.Image image = new iText.Layout.Element.Image(ImageDataFactory.Create(fileLogo));
                image.GetXObject().GetPdfObject().SetCompressionLevel(CompressionConstants.BEST_COMPRESSION);

                image.ScaleToFit(200, 35);
                float offsetX = (200 - image.GetImageScaledWidth());
                float offsetY = (523 - image.GetImageScaledHeight());
                image.SetFixedPosition(9 + offsetX, 60 + offsetY);

                //image.ScaleAbsolute(200, 40);
                //image.SetFixedPosition(PageSize.A4.Rotate().GetWidth() / 2, 800);

                doc.Add(image);
                doc.Add(new Paragraph(" "));

                #endregion


                #region Titulo de Pagina
                Paragraph title = new Paragraph();
                title.SetFont(fontTitle).SetFontSize(setFontSize).SetFontColor(ColorConstants.BLACK).SetBold()
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetUnderline()
                    .Add("SISTEMA DE INFORMACIÓN DE PLAGUICIDA\r\nReporte por Cultivo");

                doc.Add(title);

                #endregion


                #region Data

                Table tableBienesServicios = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2, 2, 2, 2, 2 })).UseAllAvailableWidth().SetVerticalBorderSpacing(3).SetHorizontalBorderSpacing(3);

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Item")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Cultivo")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Nombre Comercial")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Pais")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Titular Registro")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Plaga")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));


                int ind = 0;

                foreach (var item in model)
                {
                    ind++;

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                 .Add(new Paragraph(ind.ToString())
                 .SetTextAlignment(TextAlignment.LEFT)
                 .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                                       .Add(new Paragraph(item.IdCultivoNavigation.NombreCultivo)
                                       .SetTextAlignment(TextAlignment.LEFT)
                                       .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.IdArticuloNavigation.NombreComercial)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.IdArticuloNavigation.IdPaisNavigation.NomPais)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                                       .Add(new Paragraph(item.IdArticuloNavigation.IdTitularRegistroNavigation.NomTitularRegistro)
                                       .SetTextAlignment(TextAlignment.LEFT)
                                       .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.IdNomCientificoPlagaNavigation.NombreCientificoPlaga)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                }

                doc.Add(tableBienesServicios);
                #endregion


                #region Numeración de Página
                int numberOfPages = pdf.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {
                    doc.ShowTextAligned(new Paragraph($"Pag: {i} / {numberOfPages}"),
                        800, 590, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0).SetFontSize(setFontSize - 1);

                    doc.ShowTextAligned(new Paragraph($"Fecha y Hora de Emisión: {DateTime.Now.ToString("dd/MM/yyyy hh:mm tt")}"),
                       780, 575, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0).SetFontSize(setFontSize - 1);
                }
                #endregion

                // Ceramos el documento
                doc.Close();
            }

            var pdf64 = Convert.ToBase64String(baos.ToArray());
            GetPdfDto data = new GetPdfDto
            {
                base64 = pdf64
            };
            return data;

        }
        public async Task<GetPdfDto> GetArticulosFabricantePdfAsync(int idUsuario, string filtro)
        {
            var model = await GetArticulosFabricante(idUsuario, filtro);

            var fileLogo = System.IO.Path.Combine(_resourceDto.Documents, "images", "logo-avgust.jpg");

            PdfFont fontSubTitle = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontTitle = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontHeaderTable = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontTable = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontTexto = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontMR = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontFirma = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            float setHeight = 13f;
            float setFontSize = 7f;
            float setMinHeight = 6f;

            PdfWriter writer = null;

            var cellColor = new DeviceRgb(224, 224, 224);

            MemoryStream baos = new MemoryStream();

            using (writer = new PdfWriter(baos))
            {
                PdfDocument pdf = new PdfDocument(writer);
                Document doc = new Document(pdf, PageSize.A4.Rotate(), false);

                #region LOGO

                iText.Layout.Element.Image image = new iText.Layout.Element.Image(ImageDataFactory.Create(fileLogo));
                image.GetXObject().GetPdfObject().SetCompressionLevel(CompressionConstants.BEST_COMPRESSION);

                image.ScaleToFit(200, 35);
                float offsetX = (200 - image.GetImageScaledWidth());
                float offsetY = (523 - image.GetImageScaledHeight());
                image.SetFixedPosition(9 + offsetX, 60 + offsetY);

                //image.ScaleAbsolute(200, 40);
                //image.SetFixedPosition(PageSize.A4.Rotate().GetWidth() / 2, 800);

                doc.Add(image);
                doc.Add(new Paragraph(" "));

                #endregion


                #region Titulo de Pagina
                Paragraph title = new Paragraph();
                title.SetFont(fontTitle).SetFontSize(setFontSize).SetFontColor(ColorConstants.BLACK).SetBold()
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetUnderline()
                    .Add("SISTEMA DE INFORMACIÓN DE PLAGUICIDA\r\nReporte por Fabricantes");

                doc.Add(title);

                #endregion


                #region Data

                Table tableBienesServicios = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2, 2, 2, 2 })).UseAllAvailableWidth().SetVerticalBorderSpacing(3).SetHorizontalBorderSpacing(3);

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Item")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Fabricante")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Nombre Comercial")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Titular Registro")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Ingrediente Activo")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));


                int ind = 0;

                foreach (var item in model)
                {
                    ind++;

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                 .Add(new Paragraph(ind.ToString())
                 .SetTextAlignment(TextAlignment.LEFT)
                 .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                                       .Add(new Paragraph(item.IdFabricanteNavigation.NombreFabricante)
                                       .SetTextAlignment(TextAlignment.LEFT)
                                       .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.IdArticuloNavigation.NombreComercial)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                                       .Add(new Paragraph(item.IdArticuloNavigation.IdTitularRegistroNavigation.NomTitularRegistro)
                                       .SetTextAlignment(TextAlignment.LEFT)
                                       .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.IdArticuloNavigation.ingredientesActivos != null && item.IdArticuloNavigation.ingredientesActivos.Any() ? string.Join(Environment.NewLine, item.IdArticuloNavigation.ingredientesActivos.Select(p => $"- {p}")) : string.Empty)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                }

                doc.Add(tableBienesServicios);
                #endregion


                #region Numeración de Página
                int numberOfPages = pdf.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {
                    doc.ShowTextAligned(new Paragraph($"Pag: {i} / {numberOfPages}"),
                        800, 590, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0).SetFontSize(setFontSize - 1);

                    doc.ShowTextAligned(new Paragraph($"Fecha y Hora de Emisión: {DateTime.Now.ToString("dd/MM/yyyy hh:mm tt")}"),
                       780, 575, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0).SetFontSize(setFontSize - 1);
                }
                #endregion

                // Ceramos el documento
                doc.Close();
            }

            var pdf64 = Convert.ToBase64String(baos.ToArray());
            GetPdfDto data = new GetPdfDto
            {
                base64 = pdf64
            };
            return data;

        }
        public async Task<GetPdfDto> GetArticulosFormuladorAllPdfAsync(int idUsuario, string filtro)
        {
            var model = await GetArticulosFormuladorAll(idUsuario, filtro);

            var fileLogo = System.IO.Path.Combine(_resourceDto.Documents, "images", "logo-avgust.jpg");

            PdfFont fontSubTitle = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontTitle = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontHeaderTable = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontTable = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontTexto = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontMR = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont fontFirma = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            float setHeight = 13f;
            float setFontSize = 7f;
            float setMinHeight = 6f;

            PdfWriter writer = null;

            var cellColor = new DeviceRgb(224, 224, 224);

            MemoryStream baos = new MemoryStream();

            using (writer = new PdfWriter(baos))
            {
                PdfDocument pdf = new PdfDocument(writer);
                Document doc = new Document(pdf, PageSize.A4.Rotate(), false);

                #region LOGO

                iText.Layout.Element.Image image = new iText.Layout.Element.Image(ImageDataFactory.Create(fileLogo));
                image.GetXObject().GetPdfObject().SetCompressionLevel(CompressionConstants.BEST_COMPRESSION);

                image.ScaleToFit(200, 35);
                float offsetX = (200 - image.GetImageScaledWidth());
                float offsetY = (523 - image.GetImageScaledHeight());
                image.SetFixedPosition(9 + offsetX, 60 + offsetY);

                //image.ScaleAbsolute(200, 40);
                //image.SetFixedPosition(PageSize.A4.Rotate().GetWidth() / 2, 800);

                doc.Add(image);
                doc.Add(new Paragraph(" "));

                #endregion


                #region Titulo de Pagina
                Paragraph title = new Paragraph();
                title.SetFont(fontTitle).SetFontSize(setFontSize).SetFontColor(ColorConstants.BLACK).SetBold()
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetUnderline()
                    .Add("SISTEMA DE INFORMACIÓN DE PLAGUICIDA\r\nReporte por Formuladores");

                doc.Add(title);

                #endregion


                #region Data

                Table tableBienesServicios = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2, 2, 2, 2 })).UseAllAvailableWidth().SetVerticalBorderSpacing(3).SetHorizontalBorderSpacing(3);

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Item")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Formulador")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Nombre Comercial")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Titular Registro")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                tableBienesServicios.AddHeaderCell(new Cell().SetBackgroundColor(cellColor).SetMinHeight(setMinHeight).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Ingrediente Activo")
                    .SetFont(fontHeaderTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));


                int ind = 0;

                foreach (var item in model)
                {
                    ind++;

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                 .Add(new Paragraph(ind.ToString())
                 .SetTextAlignment(TextAlignment.LEFT)
                 .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                                       .Add(new Paragraph(item.IdFormuladorNavigation.NomFormulador)
                                       .SetTextAlignment(TextAlignment.LEFT)
                                       .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.IdProductoNavigation.NombreComercial)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                                       .Add(new Paragraph(item.IdProductoNavigation.IdTitularRegistroNavigation.NomTitularRegistro)
                                       .SetTextAlignment(TextAlignment.LEFT)
                                       .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                    tableBienesServicios.AddCell(new Cell().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(item.IdProductoNavigation.ingredientesActivos != null && item.IdProductoNavigation.ingredientesActivos.Any() ? string.Join(Environment.NewLine, item.IdProductoNavigation.ingredientesActivos.Select(p => $"- {p}")) : string.Empty)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(fontTable).SetFontSize(setFontSize - 1).SetFontColor(ColorConstants.BLACK)));

                }

                doc.Add(tableBienesServicios);
                #endregion


                #region Numeración de Página
                int numberOfPages = pdf.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {
                    doc.ShowTextAligned(new Paragraph($"Pag: {i} / {numberOfPages}"),
                        800, 590, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0).SetFontSize(setFontSize - 1);

                    doc.ShowTextAligned(new Paragraph($"Fecha y Hora de Emisión: {DateTime.Now.ToString("dd/MM/yyyy hh:mm tt")}"),
                       780, 575, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0).SetFontSize(setFontSize - 1);
                }
                #endregion

                // Ceramos el documento
                doc.Close();
            }

            var pdf64 = Convert.ToBase64String(baos.ToArray());
            GetPdfDto data = new GetPdfDto
            {
                base64 = pdf64
            };
            return data;

        }
        #endregion


        #region Calculator

        public async Task<MemoryStream> GetExcelCalculatorById(int idPedido)
        {
            var data = _calculatorUnitOfWork._simuladorPedidoRepository.GetAll(l => l.IdPedido == idPedido, includeProperties: "SimuladorPedidoItems").FirstOrDefault();
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Calculadora");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                const int startRow = 5;
                var row = startRow;
                worksheet.View.ShowGridLines = false;

                //Create Headers and format them
                worksheet.Cells["A1"].Value = "REPORTE PEDIDO SIMULADOR";
                using (var r = worksheet.Cells["A1:I1"])
                {
                    r.Merge = true;
                    r.Style.Font.Size = 13;
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(23, 55, 93));
                }

                worksheet.Cells["B3"].Value = "Id:";
                worksheet.Cells["B3"].Style.Font.Bold = true;
                worksheet.Cells["B3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                worksheet.Cells["C3"].Value = data.IdPedido;
                worksheet.Cells["C3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                worksheet.Cells["B4"].Value = "Fecha Pedido:";
                worksheet.Cells["B4"].Style.Font.Bold = true;
                worksheet.Cells["B4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                worksheet.Cells["C4"].Value = data.FechaOperacion.ToShortDateString();
                worksheet.Cells["C4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                worksheet.Cells["B5"].Value = "Importe Total (US$):";
                worksheet.Cells["B5"].Style.Font.Bold = true;
                worksheet.Cells["B5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;


                worksheet.Cells["C5"].Value = data.VentaTotal;
                worksheet.Cells["C5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                worksheet.Cells["E4"].Value = "Comision (%):";
                worksheet.Cells["E4"].Style.Font.Bold = true;
                worksheet.Cells["E4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;


                worksheet.Cells["F4"].Value = data.ComisionPercent;
                worksheet.Cells["F4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                worksheet.Cells["E5"].Value = "Comision (US$):";
                worksheet.Cells["E5"].Style.Font.Bold = true;
                worksheet.Cells["E5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                worksheet.Cells["F5"].Value = Math.Round(data.ComisionMonto, 3);
                worksheet.Cells["F5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                //worksheet.Cells["A4:F4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                //worksheet.Cells["A4:F4"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(184, 204, 228));
                //worksheet.Cells["A4:F4"].Style.Font.Bold = true;



                worksheet.Cells["A7"].Value = "Codigo";
                worksheet.Cells["B7"].Value = "Producto";
                worksheet.Cells["C7"].Value = "Familia";
                worksheet.Cells["D7"].Value = "Cantidad";
                worksheet.Cells["E7"].Value = "P.U. (US$)";
                worksheet.Cells["F7"].Value = "Importe (US$)";
                //worksheet.Cells["G7"].Value = "Costo";
                worksheet.Cells["G7"].Value = "Participación (%)";
                worksheet.Cells["H7"].Value = "Rentabilidad (%)";
                //worksheet.Cells["J7"].Value = "PesoAsignado";
                worksheet.Cells["I7"].Value = "Comision (%)";
                worksheet.Cells["A7:I7"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A7:I7"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(187, 207, 228));
                worksheet.Cells["A7:I7"].Style.Font.Bold = true;


                row = 8;
                foreach (var item in data.SimuladorPedidoItems)
                {
                    worksheet.Cells[row, 1].Value = item.Codigo;
                    worksheet.Cells[row, 2].Value = item.Producto;
                    worksheet.Cells[row, 3].Value = item.Familia;
                    worksheet.Cells[row, 4].Value = item.Cantidad;
                    worksheet.Cells[row, 5].Value = item.PrecioVvd;
                    worksheet.Cells[row, 6].Value = item.Importe;
                    worksheet.Cells[row, 7].Value = item.PartImporteTotal;
                    worksheet.Cells[row, 8].Value = item.Mb;
                    worksheet.Cells[row, 9].Value = item.ComisionPercent;
                    row++;
                }

                var sRango = "A7:I" + (row - 1).ToString();
                worksheet.Cells[sRango].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells[sRango].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[sRango].AutoFitColumns();
                worksheet.Cells[sRango].Style.HorizontalAlignment = ExcelHorizontalAlignment.General;
                worksheet.Cells[sRango].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[sRango].Style.WrapText = true;

                xlPackage.Workbook.Properties.Title = "Lista de articulos";
                xlPackage.Workbook.Properties.Author = "Israel Lozano del Castillo danielitolozano85@gmail.com";
                xlPackage.Workbook.Properties.Subject = "List de Articulos";
                xlPackage.Save();
                // Response.Clear();

            }
            stream.Position = 0;

            return stream;
        }


        #endregion
    }
}
