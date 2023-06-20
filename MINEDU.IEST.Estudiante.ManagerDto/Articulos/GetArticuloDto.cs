using IDCL.AVGUST.SIP.ManagerDto.Maestros;

namespace IDCL.AVGUST.SIP.ManagerDto.Articulos
{
    public class GetArticuloDto
    {
        public int IdArticulo { get; set; }
        public int? IdPais { get; set; }
        public string NombreComercial { get; set; }
        public int? IdTitularRegistro { get; set; }
        public string NroRegistro { get; set; }
        public int? IdTipoProducto { get; set; }
        public int? IdFormulador { get; set; }
        public int? IdGrupoQuimico { get; set; }
        public int? IdTipoFormulacion { get; set; }
        public bool FlgActivo { get; set; }
        //public GetFormuladorDto IdFormuladorNavigation { get; set; }5
        public GetGrupoQuimicoDto IdGrupoQuimicoNavigation { get; set; }
        public GetPaisDto IdPaisNavigation { get; set; }
        public GetTipoFormulacionDto IdTipoFormulacionNavigation { get; set; }
        public GetIdTipoProductoDto IdTipoProductoNavigation { get; set; }
        public GetTitularRegistroDto IdTitularRegistroNavigation { get; set; }

        public List<GetComposicionDto> Composicions { get; set; }
        public List<GetDocumentoDto> Documentos { get; set; }
        public List<GetUsoDto> Usos { get; set; }
        public List<GetCaracteristicaDto> Caracteristicas { get; set; }

        public List<GetProductoFabricanteDto> ProductoFabricantes { get; set; }
        public List<GetProductoFormuladorDto> ProductoFormuladors { get; set; }


    }



}
