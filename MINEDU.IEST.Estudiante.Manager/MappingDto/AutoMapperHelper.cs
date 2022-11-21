using AutoMapper;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using IDCL.AVGUST.SIP.ManagerDto.Articulos;
using IDCL.AVGUST.SIP.ManagerDto.Articulos.Add;
using IDCL.AVGUST.SIP.ManagerDto.Maestros;
using IDCL.AVGUST.SIP.ManagerDto.Seguridad;

namespace IDCL.AVGUST.SIP.Manager.MappingDto
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            CreateMap<Usuario, GetUsuarioDto>().ReverseMap();
            CreateMap<Pai, GetPaisDto>();
            CreateMap<Articulo, GetArticuloDto>();



            CreateMap<Formulador, GetFormuladorDto>();
            CreateMap<IdTipoProducto, GetIdTipoProductoDto>();
            CreateMap<TitularRegistro, GetTitularRegistroDto>();
            CreateMap<TipoDocumento, GetTipoDocumentoDto>();
            CreateMap<CientificoPlaga, GetCientificoPlagaDto>();
            CreateMap<Cultivo, GetCultivoDto>();
            CreateMap<Aplicacion, GetAplicacionDto>();
            CreateMap<Clase, GetClaseDto>();
            CreateMap<Toxicologica, GetToxicologicaDto>();
            CreateMap<GrupoQuimico, GetGrupoQuimicoDto>();


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

            #endregion
        }
    }
}
