using MINEDU.IEST.Estudiante.Inf_Utils.Dtos;

namespace IDCL.AVGUST.SIP.ManagerDto.Tacama
{
    public class GetUsuarioTacamaDto: Validation
    {
        public int IdPersona { get; set; }
        public string? NombreCorto { get; set; }

        public List<GetUsuarioRolTacamaDto> roles { get; set; }

    }
}
