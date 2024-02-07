using IDCL.AVGUST.SIP.Repository.Tacama;
using IDCL.Tacama.Core.Contexto.IDCL.Tacama.Core.Contexto;
using Microsoft.EntityFrameworkCore;
using MINEDU.IEST.Estudiante.Inf_Utils.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace IDLC.Tacama.Test
{
    public class TacamaTest
    {
        private readonly ITestOutputHelper _output;
        private UsuarioTacRepository _repository;
        public static DbContextOptions<DbTacamaContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=181.224.227.242,1025\\sql17;Initial Catalog=INDUSOFT_NET_ERP_TACAMA;Persist Security Info=True;User ID=sa;Password=$8732.CTIndu!";
        static TacamaTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<DbTacamaContext>()
                .UseSqlServer(connectionString)
                .Options;


        }

        public TacamaTest(ITestOutputHelper output)
        {
            var context = new DbTacamaContext(dbContextOptions);
            _repository = new UsuarioTacRepository(context);
            this._output = output;
        }

        [Fact]
        public async Task GetUsuarioClave_VendedorById()
        {
            var idPersona = 75254;
            string clave = string.Empty;
            var query = await _repository.GetCredencials(idPersona);
            if (query != null)
            {
                clave = EncryptHelper.Decrypt(query.Clave);
            }
            Assert.True(query.IdPersona > 0, $"Su clave secreta es: ");
            this._output.WriteLine($"Su clave secreta es: {clave}");

        }
    }
}
