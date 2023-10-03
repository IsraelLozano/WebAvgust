using IDCL.AVGUST.SIP.ManagerDto.Articulos;
using IDCL.AVGUST.SIP.ManagerDto.Maestros;

namespace IDCL.AVGUST.SIP.ManagerDto.Reports
{
    public class GetComposicionReportsDto
    {
        public int IdArticulo { get; set; }
        public int Iditem { get; set; }
        public int? IngredienteActivo { get; set; }
        public string FormuladorMolecular { get; set; }
        public int idGrupoQuimico { get; set; }
        public string ContracionIA { get; set; }
        public GetGrupoQuimicoDto GrupoQuimicoNavegation { get; set; }
        public GetTipoIngredienteActivoDto IngredienteActivoNavigation { get; set; }
        public GetArticuloShortDto IdArticuloNavigation { get; set; }

        public List<string> listFormuladores { get; set; }
    }
}
