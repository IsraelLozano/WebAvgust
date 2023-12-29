using IDCL.AVGUST.SIP.Entity.Pedido;
using IDCL.AVGUST.SIP.Entity.Pedido.SpEntity;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.Pedido
{
    public interface IPedidoRepository:IGenericRepository<PedidoCab>
    {
        Task<List<CostoArticulo>> ListarCostoArticulo(int idEmpresa, string codArticulo);
        Task<string> ObtenerNroPedido(int idEmpresa, int idLocal, string indCotPed);
    }
}