using IDCL.AVGUST.SIP.Entity.Pedido;
using IDCL.AVGUST.SIP.Entity.Pedido.SpEntity;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.Pedido
{
    public interface IPedidoRepository : IGenericRepository<PedidoCab>
    {
        Task<List<AvanceCobranzaZV>> ListarAvanceCobranzaZonaVendedor(int idEmpresa, string fechaInicio, string fechaFin);
        Task<List<ClientesAprobados>> ListarClientesAprobadosLCPorZona(int idEmpresa);
        Task<List<ClientesAtendidos>> ListarClientesAtendidosLCPorZona(int idEmpresa);
        Task<List<ClientesAtendidosSinLC>> ListarClientesAtendidosSinLC(int idEmpresa);
        Task<List<CostoArticulo>> ListarCostoArticulo(int idEmpresa, string codArticulo, string fechaStock);
        Task<List<CtaCteAtrazadaZona>> ListarCtaCteAtrazadaPorZona(int idEmpresa, string fechaFiltro);
        Task<List<LetraPorAceptarZona>> ListarLetraPorAceptarZona(int idEmpresa);
        Task<List<LineaCreditoDisponible>> ListarLineaCreditoDisponibleZonaCliente(int idEmpresa);
        Task<List<SegmentoZona>> ListarSegmentoZona(int idEmpresa, string fechaInicio, string fechaFin);
        Task<List<ListarTopCliente>> ListarTopCliente(int idEmpresa, string fechaInicio, string fechaFin);
        Task<List<VentaClienteProducto>> ListarVentaClienteProducto(int idEmpresa, string fechaInicio, string fechaFin);
        Task<List<VentaProducto>> ListarVentaProducto(int idEmpresa, string fechaInicio, string fechaFin);
        Task<List<ZonaVendendor>> ListarZonaVendedor(int idEmpresa, string fechaInicio, string fechaFin);
        Task<string> ObtenerNroPedido(int idEmpresa, int idLocal, string indCotPed);
    }
}