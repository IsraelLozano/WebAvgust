using IDCL.AVGUST.SIP.ManagerDto.Pedido;
using IDCL.AVGUST.SIP.ManagerDto.StoreProcedure;
using IDCL.AVGUST.SIP.ManagerDto.StoreProcedure.LineaCuentas;

namespace IDCL.AVGUST.SIP.Manager.Pedido
{
    public interface IPedidoManager
    {
        Task<AddPedidoDto> AddPedido(AddPedidoDto model);
        Task<List<GetCostoArticuloDto>> getListArticulos(int idArticulo, string codigo, string fechaStock);
        Task<List<GetAvanceCobranzaZVDto>> ListarAvanceCobranzaZonaVendedor(int idEmpresa, string fechaInicio, string fechaFin);
        Task<List<GetClientesAprobadoDto>> ListarClientesAprobadosLCPorZona(int idEmpresa);
        Task<List<GetClientesAtendidosDto>> ListarClientesAtendidosLCPorZona(int idEmpresa);
        Task<List<GetClientesAtentidosSinLCDto>> ListarClientesAtendidosSinLC(int idEmpresa);
        Task<List<GetCtaCteAtrazadaZonaDto>> ListarCtaCteAtrazadaPorZona(int idEmpresa, string fechaFiltro);
        Task<List<GetLetraPorAceptarZonaDto>> ListarLetraPorAceptarZona(int idEmpresa);
        Task<List<GetLineaCreditoDisponibleDto>> ListarLineaCreditoDisponibleZonaCliente(int idEmpresa);
        Task<List<GetListaSegmentoZonaDto>> ListarSegmentoZona(int idEmpresa, string fechaInicio, string fechaFin);
        Task<List<GetListarTopClienteDto>> ListarTopCliente(int idEmpresa, string fechaInicio, string fechaFin);
        Task<List<GetListaVentaClienteProductoDto>> ListarVentaClienteProducto(int idEmpresa, string fechaInicio, string fechaFin);
        Task<List<GetListaVentaProdutoDto>> ListarVentaProducto(int idEmpresa, string fechaInicio, string fechaFin);
        Task<List<GetListaZonaVendedorDto>> ListarZonaVendedor(int idEmpresa, string fechaInicio, string fechaFin);
    }
}