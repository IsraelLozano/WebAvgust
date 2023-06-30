using IDCL.AVGUST.SIP.ManagerDto.Calculator.Simulador;

namespace IDCL.AVGUST.SIP.Manager.Calculator
{
    public interface ISimuladorPedidoManager
    {
        Task<GetPedidoDto> addPedido(GetPedidoDto model);
        Task<List<GetPedidoDto>> getListPedidoAll();
        Task<GetPedidoDto> GetPedidoById(int id);
    }
}