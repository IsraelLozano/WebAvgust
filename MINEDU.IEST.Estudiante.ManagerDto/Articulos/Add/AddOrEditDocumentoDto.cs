using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCL.AVGUST.SIP.ManagerDto.Articulos.Add
{
    public class AddOrEditDocumentoDto
    {
        public int IdArticulo { get; set; }
        public int IdItem { get; set; }
        public int? IdTipoDocumento { get; set; }
        public DateTime? Fecha { get; set; }
        public string NomDocumento { get; set; }
    }
}
