using MINEDU.IEST.Estudiante.Inf_Utils.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCL.AVGUST.SIP.ManagerDto.Maestros
{
    public class GetTipoFormulacionDto: Validation
    {
        public int IdTipoFormulacion { get; set; }
        public string CodTipoFormulacion { get; set; }
        public string NomTipoFormulacion { get; set; }
        public bool estado { get; set; }
    }
}
