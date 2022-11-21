using IDCL.AVGUST.SIP.ManagerDto.Seguridad;

namespace IDCL.AVGUST.SIP.Manager.Seguridad
{
    public interface ISeguridadManager
    {
        Task<GetUsuarioDto> CreateOrUpdateUsuario(GetUsuarioDto model);
        Task<List<GetUsuarioDto>> GetListUsuarios();
        Task<GetUsuarioDto> GetUsuarioAutenticar(string codigo, string clave, int idPais);
        Task<GetUsuarioForEditDto> GetUsuarioById(int idUsuario);
    }
}