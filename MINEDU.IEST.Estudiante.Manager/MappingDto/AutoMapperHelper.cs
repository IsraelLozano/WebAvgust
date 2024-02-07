using AutoMapper;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using IDCL.AVGUST.SIP.Entity.Calculator;
using IDCL.AVGUST.SIP.Entity.Pedido;
using IDCL.AVGUST.SIP.Entity.Pedido.SpEntity;
using IDCL.AVGUST.SIP.ManagerDto.Articulos;
using IDCL.AVGUST.SIP.ManagerDto.Articulos.Add;
using IDCL.AVGUST.SIP.ManagerDto.Calculator;
using IDCL.AVGUST.SIP.ManagerDto.Calculator.ArticuloCalc;
using IDCL.AVGUST.SIP.ManagerDto.Calculator.ArticuloFamilia;
using IDCL.AVGUST.SIP.ManagerDto.Calculator.ListaPrecioItemDet;
using IDCL.AVGUST.SIP.ManagerDto.Calculator.ListaPreciosItem;
using IDCL.AVGUST.SIP.ManagerDto.Calculator.RentabilidadComicion;
using IDCL.AVGUST.SIP.ManagerDto.Calculator.Simulador;
using IDCL.AVGUST.SIP.ManagerDto.Maestros;
using IDCL.AVGUST.SIP.ManagerDto.Maestros.Add;
using IDCL.AVGUST.SIP.ManagerDto.Pedido;
using IDCL.AVGUST.SIP.ManagerDto.Reports;
using IDCL.AVGUST.SIP.ManagerDto.Seguridad;
using IDCL.AVGUST.SIP.ManagerDto.Seguridad.Add;
using IDCL.AVGUST.SIP.ManagerDto.StoreProcedure;
using IDCL.AVGUST.SIP.ManagerDto.StoreProcedure.LineaCuentas;
using IDCL.AVGUST.SIP.ManagerDto.Tacama;
using IDCL.AVGUST.SIP.ManagerDto.Tacama.TramaDiario;
using IDCL.Tacama.Core.Entity;

namespace IDCL.AVGUST.SIP.Manager.MappingDto
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {

            #region Generales
            CreateMap<Usuario, GetUsuarioDto>().ReverseMap();
            CreateMap<Usuario, AddOrEditUserDto>().ReverseMap();
            CreateMap<UsuarioPai, AddOrEditUsuarioPaisDto>().ReverseMap();
            CreateMap<UsuarioPai, GetOnlyUsuarioPaisDto>();
            CreateMap<UsuarioPai, GetUsuarioPaisDto>();

            CreateMap<Pai, GetPaisDto>().ReverseMap();
            CreateMap<Pai, GetPaisUsuarioDto>();

            CreateMap<Articulo, GetArticuloDto>();



            CreateMap<Formulador, GetFormuladorDto>().ReverseMap();
            CreateMap<IdTipoProducto, GetIdTipoProductoDto>().ReverseMap();
            CreateMap<TitularRegistro, GetTitularRegistroDto>().ReverseMap();
            CreateMap<TipoDocumento, GetTipoDocumentoDto>().ReverseMap();
            CreateMap<CientificoPlaga, GetCientificoPlagaDto>().ReverseMap();
            CreateMap<Cultivo, GetCultivoDto>().ReverseMap().ReverseMap();
            CreateMap<Aplicacion, GetAplicacionDto>().ReverseMap();
            CreateMap<Clase, GetClaseDto>().ReverseMap();
            CreateMap<Clase, AddClaseDto>().ReverseMap();
            CreateMap<Toxicologica, GetToxicologicaDto>().ReverseMap();
            CreateMap<GrupoQuimico, GetGrupoQuimicoDto>().ReverseMap();

            CreateMap<TipoFormulacion, GetTipoFormulacionDto>().ReverseMap();
            CreateMap<IngredienteActivo, GetTipoIngredienteActivoDto>().ReverseMap();

            CreateMap<ProductoFormulador, GetProductoFormuladorDto>().ReverseMap();
            CreateMap<ProductoFabricante, GetProductoFabricanteDto>().ReverseMap();

            CreateMap<Fabricante, GetFabricanteDto>().ReverseMap();
            CreateMap<Composicion, GetComposicionDto>();
            CreateMap<Documento, GetDocumentoDto>();
            CreateMap<Uso, GetUsoDto>();
            CreateMap<Caracteristica, GetCaracteristicaDto>();

            #endregion

            #region Articulos
            CreateMap<Articulo, AddOrEditArticuloDto>().ReverseMap();
            CreateMap<Caracteristica, AddOrEditCaracteristicaDto>().ReverseMap();
            CreateMap<Composicion, AddOrEditComposicionDto>().ReverseMap();
            CreateMap<Documento, AddOrEditDocumentoDto>().ReverseMap();
            CreateMap<Uso, AddOrEditUsoDto>().ReverseMap();
            CreateMap<ProductoFormulador, AddOrEditProductoFormuladorDto>().ReverseMap();
            CreateMap<ProductoFabricante, AddOrEditProductoFabricanteDto>().ReverseMap();

            #endregion

            #region Usuario


            #endregion

            #region Calculadora
            CreateMap<ArticuloServ, GetArticuloCalDto>().ReverseMap();
            CreateMap<ArticuloCategorium, GetArticuloCategoriDto>().ReverseMap();
            CreateMap<RentabilidadComision, GetRenatabilidadDto>().ReverseMap();
            CreateMap<ListaPrecioItem, GetListaPrecioItemDto>()
                 .ForMember(dest => dest.tienePromo, source => source.MapFrom(s => s.ListaPrecioItemDets.Any()))
                .ReverseMap();
            CreateMap<ListaPrecioItemDet, GetListaPrecioItemDetDto>()
                .ReverseMap();

            CreateMap<SimuladorPedido, GetPedidoDto>().ReverseMap();
            CreateMap<SimuladorPedidoItem, GetPedidoItemDto>().ReverseMap();

            CreateMap<TasasComision, GetTasaComisionDto>().ReverseMap();

            #endregion

            #region Reports
            CreateMap<Articulo, GetArticuloShortDto>().ReverseMap();
            CreateMap<Uso, GetPlagaReportsDto>().ReverseMap();
            CreateMap<Uso, GetCultivoReportsDto>().ReverseMap();

            CreateMap<ProductoFabricante, GetProductoFabricanteReportsDto>().ReverseMap();
            CreateMap<ProductoFormulador, GetProductoFormuladorReportsDto>().ReverseMap();
            CreateMap<Articulo, GetArticuloShortCompDto>().ReverseMap();

            CreateMap<Composicion, GetComposicionReportsDto>()

                //.ForMember(dest => dest.IdArticuloNavigation.NombreFormulador,
                //source => source.MapFrom(s => (s.IdArticuloNavigation.ProductoFormuladors != null && s.IdArticuloNavigation.ProductoFormuladors.Any()) ? s.IdArticuloNavigation.ProductoFormuladors.FirstOrDefault().IdFormuladorNavigation.NomFormulador : string.Empty))
                .ReverseMap();
            #endregion

            #region Store Procedure
            CreateMap<CostoArticulo, GetCostoArticuloDto>();


            #endregion

            #region Pedidos

            CreateMap<PedidoCab, AddPedidoDto>().ReverseMap();
            CreateMap<PedidoDet, AddPedidoDetalleDto>().ReverseMap();


            CreateMap<ListarTopCliente, GetListarTopClienteDto>().ReverseMap();
            CreateMap<SegmentoZona, GetListaSegmentoZonaDto>().ReverseMap();
            CreateMap<VentaClienteProducto, GetListaVentaClienteProductoDto>().ReverseMap();
            CreateMap<VentaProducto, GetListaVentaProdutoDto>().ReverseMap();
            CreateMap<ZonaVendendor, GetListaZonaVendedorDto>().ReverseMap();



            #endregion

            #region Lineas cuentas

            CreateMap<AvanceCobranzaZV, GetAvanceCobranzaZVDto>().ReverseMap();
            CreateMap<ClientesAprobados,GetClientesAprobadoDto>().ReverseMap();
            CreateMap<ClientesAtendidos,GetClientesAtendidosDto>().ReverseMap();
            CreateMap<ClientesAtendidosSinLC,GetClientesAtentidosSinLCDto>().ReverseMap();
            CreateMap<CtaCteAtrazadaZona,GetCtaCteAtrazadaZonaDto>().ReverseMap();
            CreateMap<LetraPorAceptarZona,GetLetraPorAceptarZonaDto>().ReverseMap();
            CreateMap<LineaCreditoDisponible,GetLineaCreditoDisponibleDto>().ReverseMap();

            #endregion

            #region Tacama
            CreateMap<Rol, GetUsuarioRolTacamaDto> ().ReverseMap();
            CreateMap<UsuarioTacama, GetUsuarioTacamaDto> ().ReverseMap();
            CreateMap<TramaDiario, GetTramaDiarioDto> ().ReverseMap();
            #endregion
        }
    }
}
