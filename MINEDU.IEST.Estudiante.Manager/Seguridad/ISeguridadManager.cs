using IDCL.AVGUST.SIP.ManagerDto.Maestros;
using IDCL.AVGUST.SIP.ManagerDto.Seguridad;
using IDCL.AVGUST.SIP.ManagerDto.Seguridad.Add;

namespace IDCL.AVGUST.SIP.Manager.Seguridad
{
    public interface ISeguridadManager
    {
        Task<AddOrEditUserDto> CreateOrUpdateUsuario(AddOrEditUserDto model);
        Task<bool> CreateOrUpdateUsuarioPais(List<AddOrEditUsuarioPaisDto> model);
        Task<GetUsuarioDto> GetForgotPassword(string email, string codigo);
        Task<List<GetPaisUsuarioDto>> GetListUsuarioPaisByIdUsuario(int idUsuario);
        Task<List<GetUsuarioDto>> GetListUsuarios();
        Task<GetUsuarioDto> GetUsuarioAutenticar(string codigo, string clave, int idPais);
        Task<GetUsuarioForEditDto> GetUsuarioById(int idUsuario);
    }
}