using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCL.AVGUST.SIP.ManagerDto.Calculator.ArticuloCalc
{
    public class GetArticuloAllDto
    {
        public List<GetArticuloCalDto> articulos { get; set; }
        public List<GetTasaComisionDto> tasaComisions { get; set; }
    }
}
