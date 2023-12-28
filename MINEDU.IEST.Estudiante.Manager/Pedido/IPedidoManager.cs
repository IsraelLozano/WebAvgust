using IDCL.AVGUST.SIP.ManagerDto.Maestros;
using IDCL.AVGUST.SIP.ManagerDto.Pedido;
using IDCL.AVGUST.SIP.ManagerDto.StoreProcedure;

namespace IDCL.AVGUST.SIP.Manager.Pedido
{
    public interface IPedidoManager
    {
        Task<AddPedidoDto> AddPedido(AddPedidoDto model);
        Task<List<GetCostoArticuloDto>> getListArticulos(int idArticulo, string codigo);
    }
}