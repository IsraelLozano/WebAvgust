using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCL.AVGUST.SIP.ManagerDto.Maestros.Add
{
    public class AddClaseDto
    {
        public int IdClase { get; set; }
        public string Descripcion { get; set; }
        public bool estado { get; set; }
        public int? IdTipoProducto { get; set; }
    }
}
