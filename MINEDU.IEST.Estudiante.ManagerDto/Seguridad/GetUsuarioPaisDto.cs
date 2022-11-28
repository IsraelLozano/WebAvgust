using IDCL.AVGUST.SIP.ManagerDto.Maestros;

namespace IDCL.AVGUST.SIP.ManagerDto.Seguridad
{
    public class GetUsuarioPaisDto
    {
        public int IdUsuario { get; set; }
        public int IdPais { get; set; }
        public bool? PorDefault { get; set; }
        public GetPaisDto IdPaisNavigation { get; set; }
    }
}
