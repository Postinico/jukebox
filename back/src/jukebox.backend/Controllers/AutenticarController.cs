using jukebox.backend.InputModels;
using jukebox.backend.Persistence;
using jukebox.backend.Repositories;

using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace jukebox.backend.Controllers
{
    [Route("api/autenticar")]
    [AllowAnonymous]
    public class AutenticarController : ControllerBase
    {
        private readonly JukeboxDbContext _dbContext;

        private readonly IUsuarioRepository _usuarioRepository;

        public AutenticarController(IConfiguration configuration, JukeboxDbContext dbContext, IUsuarioRepository usuarioRepository)
        {
            _dbContext = dbContext;

            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Iniciar sessão
        /// </summary>
        /// <remarks>Gerador de Token válido por 1 minuto</remarks>
        /// <returns>Token válido por 1 minuto</returns>
        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> IniciarSessao([FromBody] PostAutenticarInputModel model)
        {
            var usuario = _dbContext.Usuarios.Where
                (x => x.Email.ToLower() == model.Email.ToLower() &&
                 x.Senha.ToLower() == model.Senha.ToLower()).FirstOrDefault();

            if (usuario == null)
                usuario = _usuarioRepository.Obter(model.Email, model.Senha);

            if (usuario == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(usuario);

            usuario.Senha = "";

            return Ok(new
            {
                usuario = usuario,
                token = token
            });
        }
    }
}
