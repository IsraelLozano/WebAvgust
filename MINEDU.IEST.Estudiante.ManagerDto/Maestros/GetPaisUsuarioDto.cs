using IDCL.AVGUST.SIP.ManagerDto.Seguridad;

namespace IDCL.AVGUST.SIP.ManagerDto.Maestros
{
    public class GetPaisUsuarioDto
    {
        public int IdPais { get; set; }
        public string NomPais { get; set; }
        public bool tieneUsuario { get; set; }

        public List<GetOnlyUsuarioPaisDto> UsuarioPais { get; set; }

    }


}
