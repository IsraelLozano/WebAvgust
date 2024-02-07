using IDCL.AVGUST.SIP.ManagerDto.Tacama;
using IDCL.AVGUST.SIP.Repository.Tacama;
using MINEDU.IEST.Estudiante.Inf_Utils.Helpers;

namespace IDLC.Tacama.Test
{
    public class TacamaManagerTest : ITacamaManagerTest
    {
        private readonly UsuarioTacRepository _tacamaRepository;

        public TacamaManagerTest(UsuarioTacRepository tacamaRepository)
        {
            _tacamaRepository = tacamaRepository;
        }

        public async Task<string> getCredencials(int id)
        {
            string clave = string.Empty;
            var query = await _tacamaRepository.GetCredencials(id);
            if (query != null)
            {
                clave = EncryptHelper.Decrypt(query.Clave);
            }

            return clave;
        }

        public Task<GetUsuarioTacamaDto> login(string usuario, string password)
        {
            throw new NotImplementedException();
        }
    }
}
