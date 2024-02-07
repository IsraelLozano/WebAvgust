using IDCL.AVGUST.SIP.Contexto.DataPedido;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Contexto;
using IDCL.AVGUST.SIP.Contextos.IDCL.AVGUST.SIP.Contexto;
using IDCL.AVGUST.SIP.Manager.Articulos;
using IDCL.AVGUST.SIP.Manager.Calculator;
using IDCL.AVGUST.SIP.Manager.Maestro;
using IDCL.AVGUST.SIP.Manager.Pedido;
using IDCL.AVGUST.SIP.Manager.Reporte;
using IDCL.AVGUST.SIP.Manager.Seguridad;
using IDCL.AVGUST.SIP.Manager.Tacama;
using IDCL.AVGUST.SIP.Repository.Articulos;
using IDCL.AVGUST.SIP.Repository.Calculator;
using IDCL.AVGUST.SIP.Repository.Maestra;
using IDCL.AVGUST.SIP.Repository.Pedido;
using IDCL.AVGUST.SIP.Repository.Seguridad;
using IDCL.AVGUST.SIP.Repository.Tacama;
using IDCL.AVGUST.SIP.Repository.UnitOfWork;
using IDCL.AVGUST.SIP.Repository.UnitOfWork.Tacama;
using IDCL.Tacama.Core.Contexto.IDCL.Tacama.Core.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MINEDU.IEST.Estudiante.Inf_Utils.Helpers.Dapper;

namespace MINEDU.IEST.Estudiante.Inf_Apis.Extension
{
    public static class ExtensionesApi
    {
        public class RepositoriesOptions
        {
            public string ConnectionString { get; set; }
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services, Action<RepositoriesOptions> configureOptions)
        {
            var options = new RepositoriesOptions();
            configureOptions(options);

            services.AddScoped<IPaisRepository, PaisRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioPaisRepository, UsuarioPaisRepository>();

            services.AddScoped<IArticuloRepository, ArticuloRepository>();
            services.AddScoped<ICaracteristicaRepository, CaracteristicaRepository>();
            services.AddScoped<IComposicionRepository, ComposicionRepository>();
            services.AddScoped<IDocumentoRepository, DocumentoRepository>();
            services.AddScoped<IUsoRepository, UsoRepository>();

            services.AddScoped<IFormuladorRepository, FormuladoresRepository>();
            services.AddScoped<ITipoProductoRepository, TipoProductoRepository>();
            services.AddScoped<ITitularRepository, TitularRepository>();

            services.AddScoped<IAplicacionRepository, AplicacionRepository>();
            services.AddScoped<ICientificoPlagaRepository, CientificoPlagaRepository>();
            services.AddScoped<IClaseRepository, ClaseRepository>();
            services.AddScoped<ICultivoRepository, CultivoRepository>();
            services.AddScoped<IGrupoQuimicoRepository, GrupoQuimicoRepository>();
            services.AddScoped<ITipoDocumentoRepository, TipoDocumentoRepository>();
            services.AddScoped<IToxicologicaRepository, ToxicologicaRepository>();
            services.AddScoped<ITipoFormulacionRepository, TipoFormulacionRepository>();
            services.AddScoped<IIngredienteActivoRepository, IngredienteActivoRepository>();
            services.AddScoped<IFabricanteRepository, FabricanteRepository>();

            services.AddScoped<IProductoFormuladorRepository, ProductoFormuladorRepository>();
            services.AddScoped<IProductoFabricanteRepository, ProductoFabricanteRepository>();

            services.AddScoped<ArticuloUnitOfWork>();
            services.AddScoped<MaestraUnitOfWork>();
            services.AddScoped<SeguridadUnitOfWork>();
  

            services.AddDbContext<dbContextAvgust>(opt =>
            {
                opt.UseSqlServer(options.ConnectionString);
            });



            return services;
        }

        public static IServiceCollection AddRepositoriesCalculator(this IServiceCollection services, Action<RepositoriesOptions> configureOptions)
        {
            var options = new RepositoriesOptions();
            configureOptions(options);

            services.AddScoped<IArticuloCalcRepository, ArticuloCalcRepository>();
            services.AddScoped<IArticuloCategoriaRepository, ArticuloCategoriaRepository>();
            services.AddScoped<ISimuladorPedidoRepository, SimuladorPedidoRepository>();
            services.AddScoped<ISimuladorPedidoDetalleRepository, SimuladorPedidoDetalleRepository>();
            services.AddScoped<ITasaComisionRepository, TasaComisionRepository>();

            services.AddScoped<IArticuloCalculatorManager, ArticuloCalculatorManager>();
            services.AddScoped<ISimuladorPedidoManager, SimuladorPedidoManager>();

            services.AddScoped<CalculatorUnitOfWork>();

            services.AddDbContext<DbAvgustCalcContext>(opt =>
            {
                opt.UseSqlServer(options.ConnectionString);

            });

            return services;
        }
        
        public static IServiceCollection AddRepositoriesSp(this IServiceCollection services, Action<RepositoriesOptions> configureOptions)
        {
            var options = new RepositoriesOptions();
            configureOptions(options);

            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPersonaRepository, PersonaRepository>();
            services.AddScoped<ITipoCambioRepository, TipoCambioRepository>();
            services.AddScoped<IDapper, DataBase>();
            services.AddScoped<PedidoUnitOfWork>();

            services.AddDbContext<DbPedidoContext>(opt =>
            {
                opt.UseSqlServer(options.ConnectionString);

            });

            return services;
        }
        public static IServiceCollection AddManager(this IServiceCollection services)
        {

            services.AddScoped<IArticuloManager, ArticuloManager>();
            services.AddScoped<ISeguridadManager, SeguridadManager>();
            services.AddScoped<IMaestraManager, MaestraManager>();
            services.AddScoped<IReporteManager, ReporteManager>();
            services.AddScoped<IPedidoManager, PedidoManager>();

            return services;

        }
        
      
        public static IServiceCollection AddSecurityApi(this IServiceCollection services, Action<RepositoriesOptions> configureOptions)
        {
            var options = new RepositoriesOptions();
            configureOptions(options);


            //services.AddDbContext<SecurityApiDbContext>(opt =>
            //{
            //    opt.UseSqlServer(options.ConnectionString);
            //});

            return services;

        }



        #region Tacama

        public static IServiceCollection AddRepositoriesTacama(this IServiceCollection services, Action<RepositoriesOptions> configureOptions)
        {
            var options = new RepositoriesOptions();
            configureOptions(options);

            services.AddScoped<IUsuarioTacRepository, UsuarioTacRepository>();
            services.AddScoped<ITramaDiarioRepository, TramaDiarioRepository>();
            services.AddScoped<TacamaUnitOfWork>();


            services.AddDbContext<DbTacamaContext>(opt =>
            {
                opt.UseSqlServer(options.ConnectionString);
            });



            return services;
        }


        public static IServiceCollection AddManagerTacama(this IServiceCollection services)
        {

            services.AddScoped<ITacamaManager, TacamaManager>();

            return services;

        }

        #endregion
    }

}
