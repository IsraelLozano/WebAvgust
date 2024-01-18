using Dapper;
using IDCL.AVGUST.SIP.Contexto.DataPedido;
using IDCL.AVGUST.SIP.Entity.Pedido;
using IDCL.AVGUST.SIP.Entity.Pedido.SpEntity;
using MINEDU.IEST.Estudiante.Inf_Utils.Helpers.Dapper;
using MINEDU.IEST.Estudiante.Repository.Base;
using System.Data;

namespace IDCL.AVGUST.SIP.Repository.Pedido
{
    public class PedidoRepository : GenericRepository<PedidoCab>, IPedidoRepository
    {
        private readonly DbPedidoContext _context;
        private readonly IDapper _database;

        public PedidoRepository(DbPedidoContext context, IDapper database) : base(context)
        {
            this._context = context;
            this._database = database;
        }

        public async Task<List<CostoArticulo>> ListarCostoArticulo(int idEmpresa, string codArticulo, string fechaStock)
        {
            var procedureName = "usp_ApiListarCostoArticulo";
            var parameters = new DynamicParameters();
            parameters.Add("idEmpresa", idEmpresa, DbType.Int32, ParameterDirection.Input);
            parameters.Add("codArticulo", codArticulo, DbType.String, ParameterDirection.Input);
            parameters.Add("FechaStock", fechaStock, DbType.String, ParameterDirection.Input);

            var qResult = await _database.GetAll<CostoArticulo>(procedureName, parameters, CommandType.StoredProcedure);
            return qResult;
        }
        public async Task<string> ObtenerNroPedido(int idEmpresa, int idLocal, string indCotPed)
        {
            var procedureName = "usp_ObtenerNroPedido";
            var parameters = new DynamicParameters();
            parameters.Add("idEmpresa", idEmpresa, DbType.Int32, ParameterDirection.Input);
            parameters.Add("idLocal", idLocal, DbType.Int32, ParameterDirection.Input);
            parameters.Add("indCotPed", indCotPed, DbType.String, ParameterDirection.Input);
            var qResult = await _database.Get<string>(procedureName, parameters, CommandType.StoredProcedure);
            return qResult;
        }

    }
}
