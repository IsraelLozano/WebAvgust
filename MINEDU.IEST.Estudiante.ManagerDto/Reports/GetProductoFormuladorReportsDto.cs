using IDCL.AVGUST.SIP.ManagerDto.Articulos;
using IDCL.AVGUST.SIP.ManagerDto.Maestros;

namespace IDCL.AVGUST.SIP.ManagerDto.Reports
{
    public class GetProductoFormuladorReportsDto
    {
        public int IdProducto { get; set; }
        public int IdFormualdor { get; set; }

        //public Articulo IdProductoNavigation { get; set; }

        public GetFormuladorDto IdFormuladorNavigation { get; set; }

        public GetArticuloShortCompDto IdProductoNavigation { get; set; }


    }
}
