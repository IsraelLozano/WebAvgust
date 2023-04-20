using AutoMapper;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using IDCL.AVGUST.SIP.ManagerDto.Articulos;
using IDCL.AVGUST.SIP.ManagerDto.Articulos.Add;
using IDCL.AVGUST.SIP.ManagerDto.Maestros;
using IDCL.AVGUST.SIP.ManagerDto.Maestros.Add;
using IDCL.AVGUST.SIP.ManagerDto.Seguridad;
using IDCL.AVGUST.SIP.ManagerDto.Seguridad.Add;

namespace IDCL.AVGUST.SIP.Manager.MappingDto
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
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
        }
    }
}
