using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCL.AVGUST.SIP.ManagerDto.Maestros
{
    public class GetUsoDto
    {
        public int IdArticulo { get; set; }
        public int IdItem { get; set; }
        public int? IdCultivo { get; set; }
        public string NombreCientificoCultivo { get; set; }
        public int? IdNomCientificoPlaga { get; set; }
        public string Dosis { get; set; }

        public GetCultivoDto IdCultivoNavigation { get; set; }
        public GetCientificoPlagaDto IdNomCientificoPlagaNavigation { get; set; }

    }
}
