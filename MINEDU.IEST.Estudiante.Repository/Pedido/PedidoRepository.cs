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
        
        public async Task<List<ZonaVendendor>> ListarZonaVendedor(int idEmpresa, string fechaInicio, string fechaFin)
        {
            var procedureName = "usp_ApiListarZonaVendedor";
            var parameters = new DynamicParameters();
            parameters.Add("idEmpresa", idEmpresa, DbType.Int32, ParameterDirection.Input);
            parameters.Add("desde", fechaInicio, DbType.String, ParameterDirection.Input);
            parameters.Add("hasta", fechaFin, DbType.String, ParameterDirection.Input);

            var qResult = await _database.GetAll<ZonaVendendor>(procedureName, parameters, CommandType.StoredProcedure);
            return qResult;
        }
        
        public async Task<List<SegmentoZona>> ListarSegmentoZona(int idEmpresa, string fechaInicio, string fechaFin)
        {
            var procedureName = "usp_ApiListarSegmentoZona";
            var parameters = new DynamicParameters();
            parameters.Add("idEmpresa", idEmpresa, DbType.Int32, ParameterDirection.Input);
            parameters.Add("desde", fechaInicio, DbType.String, ParameterDirection.Input);
            parameters.Add("hasta", fechaFin, DbType.String, ParameterDirection.Input);

            var qResult = await _database.GetAll<SegmentoZona>(procedureName, parameters, CommandType.StoredProcedure);
            return qResult;
        }
        
        public async Task<List<ListarTopCliente>> ListarTopCliente(int idEmpresa, string fechaInicio, string fechaFin)
        {
            var procedureName = "usp_ApiListarTopCliente";
            var parameters = new DynamicParameters();
            parameters.Add("idEmpresa", idEmpresa, DbType.Int32, ParameterDirection.Input);
            parameters.Add("desde", fechaInicio, DbType.String, ParameterDirection.Input);
            parameters.Add("hasta", fechaFin, DbType.String, ParameterDirection.Input);

            var qResult = await _database.GetAll<ListarTopCliente>(procedureName, parameters, CommandType.StoredProcedure);
            return qResult;
        }
        
        public async Task<List<VentaProducto>> ListarVentaProducto(int idEmpresa, string fechaInicio, string fechaFin)
        {
            var procedureName = "usp_ApiListarVentaProducto";
            var parameters = new DynamicParameters();
            parameters.Add("idEmpresa", idEmpresa, DbType.Int32, ParameterDirection.Input);
            parameters.Add("desde", fechaInicio, DbType.String, ParameterDirection.Input);
            parameters.Add("hasta", fechaFin, DbType.String, ParameterDirection.Input);

            var qResult = await _database.GetAll<VentaProducto>(procedureName, parameters, CommandType.StoredProcedure);
            return qResult;
        }
        
        public async Task<List<VentaClienteProducto>> ListarVentaClienteProducto(int idEmpresa, string fechaInicio, string fechaFin)
        {
            var procedureName = "usp_ApiListarVentaClienteProducto";
            var parameters = new DynamicParameters();
            parameters.Add("idEmpresa", idEmpresa, DbType.Int32, ParameterDirection.Input);
            parameters.Add("desde", fechaInicio, DbType.String, ParameterDirection.Input);
            parameters.Add("hasta", fechaFin, DbType.String, ParameterDirection.Input);

            var qResult = await _database.GetAll<VentaClienteProducto>(procedureName, parameters, CommandType.StoredProcedure);
            return qResult;
        }

        #region Linea Cuentas

        public async Task<List<LineaCreditoDisponible>> ListarLineaCreditoDisponibleZonaCliente(int idEmpresa)
        {
            var procedureName = "usp_ApiListarLineaCreditoDisponibleZonaCliente";
            var parameters = new DynamicParameters();
            parameters.Add("idEmpresa", idEmpresa, DbType.Int32, ParameterDirection.Input);

            var qResult = await _database.GetAll<LineaCreditoDisponible>(procedureName, parameters, CommandType.StoredProcedure);
            return qResult;
        }
        public async Task<List<ClientesAprobados>> ListarClientesAprobadosLCPorZona(int idEmpresa)
        {
            var procedureName = "usp_ApiListarNumeroClientesAprobadosLCPorZona";
            var parameters = new DynamicParameters();
            parameters.Add("idEmpresa", idEmpresa, DbType.Int32, ParameterDirection.Input);

            var qResult = await _database.GetAll<ClientesAprobados>(procedureName, parameters, CommandType.StoredProcedure);
            return qResult;
        }
        public async Task<List<ClientesAtendidos>> ListarClientesAtendidosLCPorZona(int idEmpresa)
        {
            var procedureName = "usp_ApiListarNumeroClientesAtendidosLCPorZona";
            var parameters = new DynamicParameters();
            parameters.Add("idEmpresa", idEmpresa, DbType.Int32, ParameterDirection.Input);

            var qResult = await _database.GetAll<ClientesAtendidos>(procedureName, parameters, CommandType.StoredProcedure);
            return qResult;
        }
        public async Task<List<ClientesAtendidosSinLC>> ListarClientesAtendidosSinLC(int idEmpresa)
        {
            var procedureName = "usp_ApiListarNumeroClientesAtendidosSinLCPorZona";
            var parameters = new DynamicParameters();
            parameters.Add("idEmpresa", idEmpresa, DbType.Int32, ParameterDirection.Input);

            var qResult = await _database.GetAll<ClientesAtendidosSinLC>(procedureName, parameters, CommandType.StoredProcedure);
            return qResult;
        }
        public async Task<List<AvanceCobranzaZV>> ListarAvanceCobranzaZonaVendedor(int idEmpresa, string fechaInicio, string fechaFin)
        {
            var procedureName = "usp_ApiListarAvanceCobranzaZonaVendedor";
            var parameters = new DynamicParameters();
            parameters.Add("idEmpresa", idEmpresa, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@fecIni", fechaInicio, DbType.String, ParameterDirection.Input);
            parameters.Add("@fecFin", fechaFin, DbType.String, ParameterDirection.Input);

            var qResult = await _database.GetAll<AvanceCobranzaZV>(procedureName, parameters, CommandType.StoredProcedure);
            return qResult;
        }
        public async Task<List<CtaCteAtrazadaZona>> ListarCtaCteAtrazadaPorZona(int idEmpresa, string fechaFiltro)
        {
            var procedureName = "usp_ApiListarCtaCteAtrazadaPorZona";
            var parameters = new DynamicParameters();
            parameters.Add("idEmpresa", idEmpresa, DbType.Int32, ParameterDirection.Input);
            parameters.Add("fecFiltro", fechaFiltro, DbType.String, ParameterDirection.Input);

            var qResult = await _database.GetAll<CtaCteAtrazadaZona>(procedureName, parameters, CommandType.StoredProcedure);
            return qResult;
        }
        public async Task<List<LetraPorAceptarZona>> ListarLetraPorAceptarZona(int idEmpresa)
        {
            var procedureName = "usp_ApiListarLetraPorAceptarZona";
            var parameters = new DynamicParameters();
            parameters.Add("idEmpresa", idEmpresa, DbType.Int32, ParameterDirection.Input);

            var qResult = await _database.GetAll<LetraPorAceptarZona>(procedureName, parameters, CommandType.StoredProcedure);
            return qResult;
        }

        #endregion

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


        public async Task<List<ListaPptoVentaZonaVendedor>> ListarVentasPresupuestoZonaVendedor(int idEmpresa, string anio, string mes, int idZona)
        {
            var procedureName = "usp_ApiListarVentasPresupuestoZonaVendedor";
            var parameters = new DynamicParameters();
            parameters.Add("idEmpresa", idEmpresa, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Anio", anio, DbType.String, ParameterDirection.Input);
            parameters.Add("Mes", mes, DbType.String, ParameterDirection.Input);
            parameters.Add("idZona", idZona, DbType.String, ParameterDirection.Input);

            var qResult = await _database.GetAll<ListaPptoVentaZonaVendedor>(procedureName, parameters, CommandType.StoredProcedure);
            return qResult;
        }
        
        public async Task<List<ListarCobranzaPresupuestoZonaVendedor>> ListarCobranzaPresupuestoZonaVendedor(int idEmpresa, string anio, string mes, int idZona)
        {
            var procedureName = "usp_ApiListarCobranzaPresupuestoZonaVendedor";
            var parameters = new DynamicParameters();
            parameters.Add("idEmpresa", idEmpresa, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Anio", anio, DbType.String, ParameterDirection.Input);
            parameters.Add("Mes", mes, DbType.String, ParameterDirection.Input);
            parameters.Add("idZona", idZona, DbType.String, ParameterDirection.Input);

            var qResult = await _database.GetAll<ListarCobranzaPresupuestoZonaVendedor>(procedureName, parameters, CommandType.StoredProcedure);
            return qResult;
        }
        
        public async Task<List<ListarCreditoZonaClienteVf>> ListarCreditoZonaClienteVf(int idEmpresa, int idZona)
        {
            var procedureName = "usp_ApiListarCreditoZonaCliente";
            var parameters = new DynamicParameters();
            parameters.Add("idEmpresa", idEmpresa, DbType.Int32, ParameterDirection.Input);
            parameters.Add("idZona", idZona, DbType.String, ParameterDirection.Input);

            var qResult = await _database.GetAll<ListarCreditoZonaClienteVf>(procedureName, parameters, CommandType.StoredProcedure);
            return qResult;
        }



    }
}
