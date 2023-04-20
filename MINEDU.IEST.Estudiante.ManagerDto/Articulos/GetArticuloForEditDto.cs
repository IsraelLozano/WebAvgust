using IDCL.AVGUST.SIP.ManagerDto.Maestros;

namespace IDCL.AVGUST.SIP.ManagerDto.Articulos
{
    public class GetArticuloForEditDto
    {
        public GetArticuloDto articulo { get; set; }
        public List<GetFormuladorDto> formuladores { get; set; }
        public List<GetTitularRegistroDto> titulares { get; set; }
        public List<GetIdTipoProductoDto> tiposProductos { get; set; }
        public List<GetPaisDto> paises { get; set; }
        public List<GetTipoDocumentoDto> tiposDocumentos { get; set; }
        public List<GetCientificoPlagaDto> tiposPlagas { get; set; }
        public List<GetCultivoDto> tiposCultivos { get; set; }
        public List<GetAplicacionDto> cboAplicaciones { get; set; }
        public List<GetClaseDto> cboClase { get; set; }
        public List<GetToxicologicaDto> cboToxicologica { get; set; }
        public List<GetGrupoQuimicoDto> cboGrupoQuimico { get; set; }
        public List<GetTipoFormulacionDto> cboTipoFormulacion { get; set; }
        public List<GetTipoIngredienteActivoDto> cboTipoIngredienteActivo { get; set; }
        public List<GetFabricanteDto> cboFabricante { get; set; }

    }
}
