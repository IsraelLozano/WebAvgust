using IDCL.AVGUST.SIP.ManagerDto.Articulos;

namespace IDCL.AVGUST.SIP.Manager.Reporte
{
    public interface IReporteManager
    {
        Task<List<GetArticuloDto>> GetArticulosById(int idUsuario);
        Task<List<GetArticuloDto>> GetArticulosPorComposicion(int idUsuario);
        Task<List<GetArticuloDto>> GetArticulosPorCultivo(int idUsuario);
        Task<List<GetArticuloDto>> GetArticulosPorPlaga(int idUsuario);
        Task<MemoryStream> GetExcelArticulosGeneral(int idUsuario);
        Task<MemoryStream> GetExcelArticulosPorComposicion(int idUsuario);
        Task<MemoryStream> GetExcelArticulosPorCultivo(int idUsuario);
        Task<MemoryStream> GetExcelArticulosPorPlaga(int idUsuario);
    }
}