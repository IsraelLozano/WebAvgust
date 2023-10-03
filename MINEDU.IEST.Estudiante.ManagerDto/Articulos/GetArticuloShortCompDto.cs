using IDCL.AVGUST.SIP.ManagerDto.Maestros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCL.AVGUST.SIP.ManagerDto.Articulos
{
    public class GetArticuloShortCompDto
    {
        public int IdArticulo { get; set; }
        public string NombreComercial { get; set; }
        public GetTitularRegistroDto IdTitularRegistroNavigation { get; set; }
        public GetPaisDto IdPaisNavigation { get; set; }

        public List<string> ingredientesActivos { get; set; }
    }
}
