using IDCL.AVGUST.SIP.ManagerDto.Articulos;
using IDCL.AVGUST.SIP.ManagerDto.Maestros;

namespace IDCL.AVGUST.SIP.ManagerDto.Reports
{
    public class GetProductoFabricanteReportsDto
    {
        public int IdArticulo { get; set; }
        public int IdFabricante { get; set; }

        public GetArticuloShortCompDto IdArticuloNavigation { get; set; }

        //public Articulo IdArticuloNavigation { get; set; }
        public GetFabricanteDto IdFabricanteNavigation { get; set; }
    }
}
