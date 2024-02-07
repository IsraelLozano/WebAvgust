using IDCL.AVGUST.SIP.ManagerDto.Tacama;
using IDCL.AVGUST.SIP.ManagerDto.Tacama.TramaDiario;

namespace IDCL.AVGUST.SIP.Manager.Tacama
{
    public interface ITacamaManager
    {
        Task<GetUsuarioTacamaDto> login(string usuario, string password);
        Task<string> getCredencials(int id);
        Task<List<GetTramaDiarioDto>> GetTramaListByIdPersona(int idPersona);
        Task<GetTramaDiarioDto> AddTramaDiaria(GetTramaDiarioDto model);
    }
}