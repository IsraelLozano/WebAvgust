using IDCL.AVGUST.SIP.ManagerDto.Articulos;
using IDCL.AVGUST.SIP.ManagerDto.Maestros;

namespace IDCL.AVGUST.SIP.ManagerDto.Reports
{
    public class GetCultivoReportsDto
    {
        public int IdArticulo { get; set; }
        public int IdItem { get; set; }
        public int? IdCultivo { get; set; }
        public string NombreCientificoCultivo { get; set; }
        public int? IdNomCientificoPlaga { get; set; }
        public string Dosis { get; set; }

        public GetCultivoDto IdCultivoNavigation { get; set; }
        public GetCientificoPlagaDto IdNomCientificoPlagaNavigation { get; set; }
        public GetArticuloShortDto IdArticuloNavigation { get; set; }
    }
}
