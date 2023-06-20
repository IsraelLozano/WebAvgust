using MINEDU.IEST.Estudiante.Inf_Utils.Dtos;

namespace IDCL.AVGUST.SIP.ManagerDto.Maestros
{
    public class GetTipoIngredienteActivoDto: Validation
    {
        public int IngredenteActivo { get; set; }
        public string NomIngredienteActivo { get; set; }
        public bool estado { get; set; }
    }
}
