using MINEDU.IEST.Estudiante.Inf_Utils.Dtos;

namespace IDCL.AVGUST.SIP.ManagerDto.Maestros
{
    public class GetTitularRegistroDto: Validation
    {
        public int IdTitularRegistro { get; set; }
        public string NomTitularRegistro { get; set; }
        public bool estado { get; set; }
    }
}
