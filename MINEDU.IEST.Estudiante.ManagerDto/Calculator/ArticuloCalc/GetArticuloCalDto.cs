using IDCL.AVGUST.SIP.ManagerDto.Calculator.ArticuloFamilia;
using IDCL.AVGUST.SIP.ManagerDto.Calculator.ListaPreciosItem;
using IDCL.AVGUST.SIP.ManagerDto.Calculator.RentabilidadComicion;

namespace IDCL.AVGUST.SIP.ManagerDto.Calculator.ArticuloCalc
{
    public class GetArticuloCalDto
    {
        public int IdEmpresa { get; set; }
        public int IdArticulo { get; set; }
        public string? CodArticulo { get; set; }
        public string? NomArticulo { get; set; }
        public string? NomArticuloEng { get; set; }
        public string? NomArticuloLargo { get; set; }
        public string? NomCorto { get; set; }
        public bool? IndCodBarra { get; set; }
        public string? CodBarra { get; set; }

        public GetArticuloCategoriDto? ArticuloCategorium { get; set; }
        public List<GetListaPrecioItemDto> ListaPrecioItems { get; set; }
        public List<GetRenatabilidadDto> RentabilidadComisions { get; set; }

    }
}
