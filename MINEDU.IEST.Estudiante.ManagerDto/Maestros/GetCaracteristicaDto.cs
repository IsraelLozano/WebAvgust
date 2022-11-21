using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCL.AVGUST.SIP.ManagerDto.Maestros
{
    public class GetCaracteristicaDto
    {
        public int IdArticulo { get; set; }
        public int IdItem { get; set; }
        public int? IdAplicacion { get; set; }
        public int? IdClase { get; set; }
        public int? IdToxicologica { get; set; }

        public GetAplicacionDto IdAplicacionNavigation { get; set; }
        public GetClaseDto IdClaseNavigation { get; set; }
        public GetToxicologicaDto IdToxicologicaNavigation { get; set; }
    }
}
