using MINEDU.IEST.Estudiante.Inf_Utils.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCL.AVGUST.SIP.ManagerDto.Maestros
{
    public class GetToxicologicaDto: Validation
    {
        public int IdToxicologica { get; set; }
        public string Descripcion { get; set; }
        public bool estado { get; set; }
    }
}
