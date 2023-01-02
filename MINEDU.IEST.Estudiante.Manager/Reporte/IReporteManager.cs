using IDCL.AVGUST.SIP.ManagerDto.Articulos;

namespace IDCL.AVGUST.SIP.Manager.Reporte
{
    public interface IReporteManager
    {
        Task<List<GetArticuloDto>> GetArticulosById(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo);
        Task<List<GetArticuloDto>> GetArticulosPorComposicion(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo);
        Task<List<GetArticuloDto>> GetArticulosPorCultivo(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo);
        Task<List<GetArticuloDto>> GetArticulosPorPlaga(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo);
        Task<MemoryStream> GetExcelArticulosGeneral(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo);
        Task<MemoryStream> GetExcelArticulosPorComposicion(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo);
        Task<MemoryStream> GetExcelArticulosPorCultivo(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo);
        Task<MemoryStream> GetExcelArticulosPorPlaga(int idUsuario, int tipoFiltro, string filtro, int idIngredienteActivo);
    }
}