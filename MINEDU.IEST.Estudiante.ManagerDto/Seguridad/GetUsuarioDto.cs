namespace IDCL.AVGUST.SIP.ManagerDto.Seguridad
{
    public class GetUsuarioDto
    {
        public int IdUsuario { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public string Credencial { get; set; }
        public string Email { get; set; }

        public List<GetUsuarioPaisDto> UsuarioPais { get; set; }


    }
}
