using IDCL.AVGUST.SIP.ManagerDto.Articulos;
using IDCL.AVGUST.SIP.ManagerDto.Reports;
using MINEDU.IEST.Estudiante.Inf_Utils.Dtos;

namespace IDCL.AVGUST.SIP.Manager.Reporte
{
    public interface IReporteManager
    {
        Task<List<GetArticuloDto>> GetArticulosById(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo);
        Task<List<GetComposicionReportsDto>> GetArticulosPorComposicion(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo);
        Task<List<GetCultivoReportsDto>> GetArticulosPorCultivo(int idUsuario, string filtro);
        Task<List<GetPlagaReportsDto>> GetArticulosPorPlaga(int idUsuario, string filtro);
        Task<MemoryStream> GetExcelArticulosGeneral(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo);
        Task<MemoryStream> GetExcelArticulosPorComposicion(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo);
        Task<MemoryStream> GetExcelArticulosPorCultivo(int idUsuario, string filtro);
        Task<MemoryStream> GetExcelArticulosPorPlaga(int idUsuario, string filtro);

        #region Reportes en PDF
        Task<GetPdfDto> GetProductosFormuladosPdfAsync(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo);
        Task<GetPdfDto> GetArticulosPorComposicionPdfAsync(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo);
        Task<GetPdfDto> GetArticulosPorCultivoPdfAsync(int idUsuario, string filtro);
        Task<GetPdfDto> GetArticulosPorPlagaPdfAsync(int idUsuario, string filtro);
        Task<List<GetProductoFabricanteReportsDto>> GetArticulosFabricante(int idUsuario, string filtro);
        Task<List<GetProductoFormuladorReportsDto>> GetArticulosFormuladorAll(int idUsuario, string filtro);
        Task<GetPdfDto> GetArticulosFabricantePdfAsync(int idUsuario, string filtro);
        Task<GetPdfDto> GetArticulosFormuladorAllPdfAsync(int idUsuario, string filtro);
        Task<MemoryStream> GetExcelGetArticulosFabricante(int idUsuario, string filtro);
        Task<MemoryStream> GetExcelGetArticulosFormuladorAll(int idUsuario, string filtro);
        Task<MemoryStream> GetExcelCalculatorById(int idPedido);

        #endregion
    }
}