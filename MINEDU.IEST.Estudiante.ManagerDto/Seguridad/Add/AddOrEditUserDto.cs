namespace IDCL.AVGUST.SIP.ManagerDto.Seguridad.Add
{
    public class AddOrEditUserDto
    {
        public int IdUsuario { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public string Credencial { get; set; }
        public string TextoClave { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }

        public string Email { get; set; }

    }
}
