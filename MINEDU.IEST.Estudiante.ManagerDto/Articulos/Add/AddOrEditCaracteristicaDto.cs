using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCL.AVGUST.SIP.ManagerDto.Articulos.Add
{
    public class AddOrEditCaracteristicaDto
    {
        public int IdArticulo { get; set; }
        public int IdItem { get; set; }
        public int? IdAplicacion { get; set; }
        public int? IdClase { get; set; }
        public int? IdToxicologica { get; set; }
    }
}
