using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Contexto;
using IDCL.AVGUST.SIP.Manager.Articulos;
using IDCL.AVGUST.SIP.Manager.Maestro;
using IDCL.AVGUST.SIP.Manager.Seguridad;
using IDCL.AVGUST.SIP.Repository.Articulos;
using IDCL.AVGUST.SIP.Repository.Maestra;
using IDCL.AVGUST.SIP.Repository.Seguridad;
using IDCL.AVGUST.SIP.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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




            services.AddScoped<ArticuloUnitOfWork>();
            services.AddScoped<MaestraUnitOfWork>();
            services.AddScoped<SeguridadUnitOfWork>();


            services.AddDbContext<dbContextAvgust>(opt =>
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


    }

}
