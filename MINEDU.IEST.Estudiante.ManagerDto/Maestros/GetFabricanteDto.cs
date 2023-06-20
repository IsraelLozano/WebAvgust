using MINEDU.IEST.Estudiante.Inf_Utils.Dtos;

namespace IDCL.AVGUST.SIP.ManagerDto.Maestros
{
    public class GetFabricanteDto: Validation
    {
        public int IdFabricante { get; set; }
        public string NombreFabricante { get; set; }
        public bool Estado { get; set; }

    }
}
