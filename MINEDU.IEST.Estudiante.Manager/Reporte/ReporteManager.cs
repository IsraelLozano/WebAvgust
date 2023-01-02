using AutoMapper;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using IDCL.AVGUST.SIP.ManagerDto.Articulos;
using IDCL.AVGUST.SIP.Repository.UnitOfWork;
using MINEDU.IEST.Estudiante.Inf_Utils.Dtos;
using MINEDU.IEST.Estudiante.Inf_Utils.Enumerados;
using MINEDU.IEST.Estudiante.Inf_Utils.Helpers.FileManager;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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

        public ReporteManager(IMapper mapper, ArticuloUnitOfWork articuloUnitOfWork, MaestraUnitOfWork maestraUnitOfWork, ResourceDto resourceDto, IStorageManager storageManager, SeguridadUnitOfWork seguridadUnitOfWork)
        {
            _mapper = mapper;
            _articuloUnitOfWork = articuloUnitOfWork;
            _maestraUnitOfWork = maestraUnitOfWork;
            _resourceDto = resourceDto;
            _storageManager = storageManager;
            _seguridadUnitOfWork = seguridadUnitOfWork;
        }


        #region Querys
        public async Task<List<GetArticuloDto>> GetArticulosById(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo)
        {
            var user = _seguridadUnitOfWork._usuarioRepositoy.GetAll(p => p.IdUsuario == idUsuario, includeProperties: "UsuarioPais,UsuarioPais.IdPaisNavigation").FirstOrDefault();
            var paises = user.UsuarioPais.Select(p => p.IdPais).ToList();

            var filter = new List<Articulo>();

            if ((int)TipoBusquedaArticulo.nombre == tipoFiltro)
            {
                var query = _articuloUnitOfWork._articuloRepository.GetAll(p => paises.Contains(p.IdPais.Value) && p.FlgActivo,
              includeProperties: "IdFormuladorNavigation,IdGrupoQuimicoNavigation,IdPaisNavigation,IdTipoProductoNavigation,IdTitularRegistroNavigation,IdTipoFormulacionNavigation,Composicions,Documentos,Usos,Caracteristicas,Composicions.GrupoQuimicoNavegation," +
              "Composicions.IngredienteActivoNavigation," +
              "Documentos.IdTipoDocumentoNavigation," +
              "Usos.IdCultivoNavigation," +
              "Usos.IdNomCientificoPlagaNavigation," +
              "Caracteristicas.IdClaseNavigation," +
              "Caracteristicas.IdToxicologicaNavigation",
              orderBy: p => p.OrderByDescending(l => l.IdArticulo)).AsEnumerable();

                filter = query.Where(p => filtro.Contains(p.NombreComercial, StringComparison.CurrentCultureIgnoreCase) || p.NombreComercial.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();

            }
            else if ((int)TipoBusquedaArticulo.ingredienteActivo == tipoFiltro)
            {
                var query = _articuloUnitOfWork._articuloRepository.GetAll(p => paises.Contains(p.IdPais.Value) && p.FlgActivo && p.Composicions.Any(l => l.IngredienteActivo == idIngredienteActivo),
             includeProperties: "IdFormuladorNavigation,IdGrupoQuimicoNavigation,IdPaisNavigation,IdTipoProductoNavigation,IdTitularRegistroNavigation,IdTipoFormulacionNavigation,Composicions,Documentos,Usos,Caracteristicas,Composicions.GrupoQuimicoNavegation," +
             "Composicions.IngredienteActivoNavigation," +
             "Documentos.IdTipoDocumentoNavigation," +
             "Usos.IdCultivoNavigation," +
             "Usos.IdNomCientificoPlagaNavigation," +
             "Caracteristicas.IdClaseNavigation," +
             "Caracteristicas.IdToxicologicaNavigation", orderBy: p => p.OrderByDescending(l => l.IdArticulo)).AsEnumerable();
                filter = query.ToList();
            }

            var response = _mapper.Map<List<GetArticuloDto>>(filter);
            return response;
        }
        public async Task<List<GetArticuloDto>> GetArticulosPorComposicion(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo)
        {
            var user = _seguridadUnitOfWork._usuarioRepositoy.GetAll(p => p.IdUsuario == idUsuario, includeProperties: "UsuarioPais,UsuarioPais.IdPaisNavigation").FirstOrDefault();
            var paises = user.UsuarioPais.Select(p => p.IdPais).ToList();

            var filter = new List<Articulo>();

            if ((int)TipoBusquedaArticulo.nombre == tipoFiltro)
            {
                var query = _articuloUnitOfWork._articuloRepository.GetAll(p => paises.Contains(p.IdPais.Value) && p.FlgActivo,
           includeProperties: "IdFormuladorNavigation,IdTitularRegistroNavigation,Composicions,Composicions.GrupoQuimicoNavegation," +
           "Composicions.IngredienteActivoNavigation",
           orderBy: p => p.OrderByDescending(l => l.IdArticulo)).AsEnumerable();

                filter = query.Where(p => filtro.Contains(p.NombreComercial, StringComparison.CurrentCultureIgnoreCase) || p.NombreComercial.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else if ((int)TipoBusquedaArticulo.ingredienteActivo == tipoFiltro)
            {
                var query = _articuloUnitOfWork._articuloRepository.GetAll(p => paises.Contains(p.IdPais.Value) && p.FlgActivo && p.Composicions.Any(l => l.IngredienteActivo == idIngredienteActivo),
                    includeProperties: "IdFormuladorNavigation,IdTitularRegistroNavigation,Composicions,Composicions.GrupoQuimicoNavegation," +
         "Composicions.IngredienteActivoNavigation",
                    orderBy: p => p.OrderByDescending(l => l.IdArticulo)).AsEnumerable();

                filter = query.ToList();
            }

            var response = _mapper.Map<List<GetArticuloDto>>(filter);
            return response;
        }
        public async Task<List<GetArticuloDto>> GetArticulosPorPlaga(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo)
        {
            var user = _seguridadUnitOfWork._usuarioRepositoy.GetAll(p => p.IdUsuario == idUsuario, includeProperties: "UsuarioPais,UsuarioPais.IdPaisNavigation").FirstOrDefault();
            var paises = user.UsuarioPais.Select(p => p.IdPais).ToList();

            var filter = new List<Articulo>();

            if ((int)TipoBusquedaArticulo.nombre == tipoFiltro)
            {
                var query = _articuloUnitOfWork._articuloRepository.GetAll(p => paises.Contains(p.IdPais.Value) && p.FlgActivo,
                    includeProperties: "IdPaisNavigation,IdTitularRegistroNavigation,Usos,Usos.IdCultivoNavigation,Usos.IdNomCientificoPlagaNavigation",
                    orderBy: p => p.OrderByDescending(l => l.IdArticulo)).AsEnumerable();

                filter = query.Where(p => filtro.Contains(p.NombreComercial, StringComparison.CurrentCultureIgnoreCase) || p.NombreComercial.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else if ((int)TipoBusquedaArticulo.ingredienteActivo == tipoFiltro)
            {
                var query = _articuloUnitOfWork._articuloRepository.GetAll(p => paises.Contains(p.IdPais.Value) && p.FlgActivo && p.Composicions.Any(l => l.IngredienteActivo == idIngredienteActivo),
                    includeProperties: "IdPaisNavigation,IdTitularRegistroNavigation,Usos,Usos.IdCultivoNavigation,Usos.IdNomCientificoPlagaNavigation",
                    orderBy: p => p.OrderByDescending(l => l.IdArticulo)).AsEnumerable();

                filter = query.ToList();
            }


            var response = _mapper.Map<List<GetArticuloDto>>(filter);
            return response;
        }
        public async Task<List<GetArticuloDto>> GetArticulosPorCultivo(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo)
        {
            var user = _seguridadUnitOfWork._usuarioRepositoy.GetAll(p => p.IdUsuario == idUsuario, includeProperties: "UsuarioPais,UsuarioPais.IdPaisNavigation").FirstOrDefault();
            var paises = user.UsuarioPais.Select(p => p.IdPais).ToList();

            var filter = new List<Articulo>();

            if ((int)TipoBusquedaArticulo.nombre == tipoFiltro)
            {
                var query = _articuloUnitOfWork._articuloRepository.GetAll(p => paises.Contains(p.IdPais.Value) && p.FlgActivo,
                    includeProperties: "IdPaisNavigation,IdTitularRegistroNavigation,Caracteristicas,Caracteristicas.IdClaseNavigation,Usos,Usos.IdCultivoNavigation," +
            "Usos.IdNomCientificoPlagaNavigation,Caracteristicas.IdToxicologicaNavigation",
                    orderBy: p => p.OrderByDescending(l => l.IdArticulo)).AsEnumerable();

                filter = query.Where(p => filtro.Contains(p.NombreComercial, StringComparison.CurrentCultureIgnoreCase) || p.NombreComercial.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else if ((int)TipoBusquedaArticulo.ingredienteActivo == tipoFiltro)
            {
                var query = _articuloUnitOfWork._articuloRepository.GetAll(p => paises.Contains(p.IdPais.Value) && p.FlgActivo && p.Composicions.Any(l => l.IngredienteActivo == idIngredienteActivo),
                  includeProperties: "IdPaisNavigation,IdTitularRegistroNavigation,Caracteristicas,Caracteristicas.IdClaseNavigation,Usos,Usos.IdCultivoNavigation," +
          "Usos.IdNomCientificoPlagaNavigation,Caracteristicas.IdToxicologicaNavigation",
                  orderBy: p => p.OrderByDescending(l => l.IdArticulo)).AsEnumerable();
                filter = query.ToList();
            }

            var response = _mapper.Map<List<GetArticuloDto>>(filter);
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
                namedStyle.Style.Font.Color.SetColor(Color.Blue);
                const int startRow = 5;
                var row = startRow;
                worksheet.View.ShowGridLines = false;
                //Create Headers and format them
                worksheet.Cells["A1"].Value = "REPORTE DE PRODUCTOS FORMULADOS";
                using (var r = worksheet.Cells["A1:M1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(Color.White);
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
                }


                worksheet.Cells["A4"].Value = "Nombre Comercial";
                worksheet.Cells["B4"].Value = "Nro Registro";
                worksheet.Cells["C4"].Value = "Pais";
                worksheet.Cells["D4"].Value = "Titular Registro";
                worksheet.Cells["E4"].Value = "Tipo Producto";
                worksheet.Cells["F4"].Value = "Tipo Formulacion";
                worksheet.Cells["G4"].Value = "Formulador";
                worksheet.Cells["H4"].Value = "Ingrediente Activo";
                worksheet.Cells["I4"].Value = "Concentracion (IA)";
                worksheet.Cells["J4"].Value = "Toxicologia";
                worksheet.Cells["K4"].Value = "Cultivo";
                worksheet.Cells["L4"].Value = "Plaga";
                worksheet.Cells["M4"].Value = "Dosis";
                worksheet.Cells["A4:M4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A4:M4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                worksheet.Cells["A4:M4"].Style.Font.Bold = true;


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
                    worksheet.Cells[row, 7].Value = item.IdFormuladorNavigation.NomFormulador;
                    worksheet.Cells[row, 8].Value = "'" + string.Join(Environment.NewLine, item.Composicions.Select(p => $"- {p.IngredienteActivoNavigation.NomIngredienteActivo}"));
                    worksheet.Cells[row, 9].Value = "'" + string.Join(Environment.NewLine, item.Composicions.Select(p => $"- {p.ContracionIA}"));
                    worksheet.Cells[row, 10].Value = "'" + string.Join(Environment.NewLine, item.Caracteristicas.Select(p => $"- {p.IdToxicologicaNavigation.Descripcion}"));
                    worksheet.Cells[row, 11].Value = "'" + string.Join(Environment.NewLine, item.Usos.Select(p => $"- {p.IdCultivoNavigation.NombreCultivo}"));
                    worksheet.Cells[row, 12].Value = "'" + string.Join(Environment.NewLine, item.Usos.Select(p => $"- {p.IdNomCientificoPlagaNavigation.NombreCientificoPlaga}"));
                    worksheet.Cells[row, 13].Value = "Ver Etiqueta";

                    row++;
                }
                var sRango = "A4:M" + (row - 1).ToString();
                worksheet.Cells[sRango].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells[sRango].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[sRango].AutoFitColumns();
                worksheet.Cells[sRango].Style.HorizontalAlignment = ExcelHorizontalAlignment.General;
                worksheet.Cells[sRango].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[sRango].Style.WrapText = false;

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
                namedStyle.Style.Font.Color.SetColor(Color.Blue);
                const int startRow = 5;
                var row = startRow;
                worksheet.View.ShowGridLines = false;

                //Create Headers and format them
                worksheet.Cells["A1"].Value = "REPORTE POR COMPOSICIÓN";
                using (var r = worksheet.Cells["A1:F1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(Color.White);
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
                }

                worksheet.Cells["A4"].Value = "Nombre Comercial";
                worksheet.Cells["B4"].Value = "Ingrediente Activo";
                worksheet.Cells["C4"].Value = "Concentracion (IA)";
                worksheet.Cells["D4"].Value = "Grupo Quimico";
                worksheet.Cells["E4"].Value = "Titular Registro";
                worksheet.Cells["F4"].Value = "Formulador";
                worksheet.Cells["A4:F4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A4:F4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                worksheet.Cells["A4:F4"].Style.Font.Bold = true;


                row = 5;
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.NombreComercial;
                    worksheet.Cells[row, 2].Value = "'" + string.Join(Environment.NewLine, item.Composicions.Select(p => $"- {p.IngredienteActivoNavigation.NomIngredienteActivo}"));
                    worksheet.Cells[row, 3].Value = "'" + string.Join(Environment.NewLine, item.Composicions.Select(p => $"- {p.ContracionIA}"));
                    worksheet.Cells[row, 4].Value = "'" + string.Join(Environment.NewLine, item.Composicions.Select(p => $"- {p.GrupoQuimicoNavegation.NomGrupoQuimico}"));
                    worksheet.Cells[row, 5].Value = item.IdTitularRegistroNavigation.NomTitularRegistro;
                    worksheet.Cells[row, 6].Value = item.IdFormuladorNavigation.NomFormulador;

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
                worksheet.Cells[sRango].Style.WrapText = false;

                xlPackage.Workbook.Properties.Title = "Lista de articulos";
                xlPackage.Workbook.Properties.Author = "Israel Lozano del Castillo danielitolozano85@gmail.com";
                xlPackage.Workbook.Properties.Subject = "List de Articulos";
                xlPackage.Save();
                // Response.Clear();

            }
            stream.Position = 0;

            return stream;
        }
        public async Task<MemoryStream> GetExcelArticulosPorPlaga(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo)
        {
            var data = await this.GetArticulosPorPlaga(idUsuario, tipoFiltro, filtro, idIngredienteActivo);
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("articulos");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(Color.Blue);
                const int startRow = 5;
                var row = startRow;
                worksheet.View.ShowGridLines = false;

                //Create Headers and format them
                worksheet.Cells["A1"].Value = "REPORTE POR PLAGA";
                using (var r = worksheet.Cells["A1:F1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(Color.White);
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
                }

                worksheet.Cells["A4"].Value = "Plaga";
                worksheet.Cells["B4"].Value = "Nombre Comercial";
                worksheet.Cells["C4"].Value = "Cultivo";
                worksheet.Cells["D4"].Value = "Dosis";
                worksheet.Cells["E4"].Value = "Titular Registro";
                worksheet.Cells["F4"].Value = "Pais";
                worksheet.Cells["A4:F4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A4:F4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                worksheet.Cells["A4:F4"].Style.Font.Bold = true;


                row = 5;
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = "'" + string.Join(Environment.NewLine, item.Usos.Select(p => $"- {p.IdNomCientificoPlagaNavigation.NombreCientificoPlaga}"));
                    worksheet.Cells[row, 2].Value = item.NombreComercial;
                    worksheet.Cells[row, 3].Value = "'" + string.Join(Environment.NewLine, item.Usos.Select(p => $"- {p.IdCultivoNavigation.NombreCultivo}"));
                    worksheet.Cells[row, 4].Value = "Ver Etiqueta";
                    worksheet.Cells[row, 5].Value = item.IdTitularRegistroNavigation.NomTitularRegistro;
                    worksheet.Cells[row, 6].Value = item.IdPaisNavigation.NomPais;
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
                worksheet.Cells[sRango].Style.WrapText = false;

                xlPackage.Workbook.Properties.Title = "Lista de articulos";
                xlPackage.Workbook.Properties.Author = "Israel Lozano del Castillo danielitolozano85@gmail.com";
                xlPackage.Workbook.Properties.Subject = "List de Articulos";
                xlPackage.Save();
                // Response.Clear();

            }
            stream.Position = 0;

            return stream;
        }
        public async Task<MemoryStream> GetExcelArticulosPorCultivo(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo)
        {
            var data = await this.GetArticulosPorCultivo(idUsuario, tipoFiltro, filtro, idIngredienteActivo);
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("articulos");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(Color.Blue);
                const int startRow = 5;
                var row = startRow;
                worksheet.View.ShowGridLines = false;

                //Create Headers and format them
                worksheet.Cells["A1"].Value = "REPORTE POR CULTIVO";
                using (var r = worksheet.Cells["A1:F1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(Color.White);
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
                }

                worksheet.Cells["A4"].Value = "Cultivo";
                worksheet.Cells["B4"].Value = "Nombre Comercial";
                worksheet.Cells["C4"].Value = "Plaga";
                worksheet.Cells["D4"].Value = "Dosis";
                worksheet.Cells["E4"].Value = "Titular Registro";
                worksheet.Cells["F4"].Value = "Pais";
                worksheet.Cells["A4:F4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A4:F4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                worksheet.Cells["A4:F4"].Style.Font.Bold = true;


                row = 5;
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = "'" + string.Join(Environment.NewLine, item.Usos.Select(p => $"- {p.IdCultivoNavigation.NombreCultivo}"));
                    worksheet.Cells[row, 2].Value = item.NombreComercial;
                    worksheet.Cells[row, 3].Value = "'" + string.Join(Environment.NewLine, item.Usos.Select(p => $"- {p.IdNomCientificoPlagaNavigation.NombreCientificoPlaga}"));
                    worksheet.Cells[row, 4].Value = "Ver Etiqueta";
                    worksheet.Cells[row, 5].Value = item.IdTitularRegistroNavigation.NomTitularRegistro;
                    worksheet.Cells[row, 6].Value = item.IdPaisNavigation.NomPais;
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
                worksheet.Cells[sRango].Style.WrapText = false;

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
