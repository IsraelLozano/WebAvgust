using MINEDU.IEST.Estudiante.Inf_Utils.Dtos;

namespace IDCL.AVGUST.SIP.ManagerDto.Maestros
{
    public class GetCientificoPlagaDto: Validation
    {
        public int IdNomCientificoPlaga { get; set; }
        public string NombreCientificoPlaga { get; set; }
        public bool estado { get; set; }
    }
}
