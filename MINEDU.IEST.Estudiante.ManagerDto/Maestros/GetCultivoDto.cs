using MINEDU.IEST.Estudiante.Inf_Utils.Dtos;

namespace IDCL.AVGUST.SIP.ManagerDto.Maestros
{
    public class GetCultivoDto:Validation
    {
        public int IdCultivo { get; set; }
        public string NombreCultivo { get; set; }
        public bool estado { get; set; }
        public string NombreComun { get; set; }
    }
}
