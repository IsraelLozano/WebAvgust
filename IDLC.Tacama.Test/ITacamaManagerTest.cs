using IDCL.AVGUST.SIP.ManagerDto.Tacama;

namespace IDLC.Tacama.Test
{
    public interface ITacamaManagerTest
    {
        Task<GetUsuarioTacamaDto> login(string usuario, string password);

        Task<string> getCredencials(int id);
    }
}
