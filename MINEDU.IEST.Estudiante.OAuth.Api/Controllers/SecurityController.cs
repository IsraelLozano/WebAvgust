using IDCL.AVGUST.SIP.Manager.Maestro;
using IDCL.AVGUST.SIP.Manager.Seguridad;
using IDCL.AVGUST.SIP.ManagerDto.Seguridad.Add;
using Microsoft.AspNetCore.Mvc;
using MINEDU.IEST.Estudiante.Inf_Utils.Constants;

namespace MINEDU.IEST.Estudiante.OAuth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ILogger<SecurityController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISeguridadManager _seguridadManager;
        private readonly IMaestraManager _maestraManager;

        public SecurityController(ILogger<SecurityController> logger, IHttpContextAccessor httpContextAccessor, ISeguridadManager seguridadManager, IMaestraManager maestraManager)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            this._seguridadManager = seguridadManager;
            this._maestraManager = maestraManager;
        }

        [HttpGet("getaccess")]
        public async Task<IActionResult> GetUserByUserName()
        {
            int personId = int.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == SecurityClaimType.PersonId).Value);
            //if (query == null)
            //    return NotFound();

            return Ok();
        }


        [HttpGet("getpais")]
        public async Task<IActionResult> GetPais()
        {
            var query = await _maestraManager.getListPais();
            return Ok(query);
        }

        [HttpGet("oauth-login")]
        public async Task<IActionResult> oauthLogin(string codigo, string clave, int idPais)
        {
            var resp = await _seguridadManager.GetUsuarioAutenticar(codigo, clave, idPais);
            return Ok(resp);
        }

        [HttpGet("users")]
        public async Task<IActionResult> listUsers()
        {
            var resp = await _seguridadManager.GetListUsuarios();
            return Ok(resp);
        }

        [HttpGet("user/{id:int}")]
        public async Task<IActionResult> getUserById(int id)
        {
            var resp = await _seguridadManager.GetUsuarioById(id);
            return Ok(resp);
        }

        [HttpPost("CreateOrUpdateUsuario")]
        public async Task<IActionResult> AddUsuario(AddOrEditUserDto model)
        {

            return Ok(await _seguridadManager.CreateOrUpdateUsuario(model));
        }



        #region Usuario - Pais
        [HttpGet("usuarioPais/{id:int}")]
        public async Task<IActionResult> GetUsuarioPaisByIdUsuario(int id)
        {
            var resp = await _seguridadManager.GetListUsuarioPaisByIdUsuario(id);
            return Ok(resp);
        }

        [HttpPost("CreateOrUpdateUsuarioPais")]
        public async Task<IActionResult> AddUsuario(List<AddOrEditUsuarioPaisDto> model)
        {

            return Ok(await _seguridadManager.CreateOrUpdateUsuarioPais(model));
        }

        #endregion


    }
}
