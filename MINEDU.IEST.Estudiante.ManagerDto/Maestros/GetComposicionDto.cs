using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCL.AVGUST.SIP.ManagerDto.Maestros
{
    public class GetComposicionDto
    {
        public int IdArticulo { get; set; }
        public int Iditem { get; set; }
        public int? IngredienteActivo { get; set; }
        public string FormuladorMolecular { get; set; }
        public int idGrupoQuimico { get; set; }
        public string ContracionIA { get; set; }
        public GetGrupoQuimicoDto GrupoQuimicoNavegation { get; set; }
        public GetTipoIngredienteActivoDto IngredienteActivoNavigation { get; set; }
    }
}
