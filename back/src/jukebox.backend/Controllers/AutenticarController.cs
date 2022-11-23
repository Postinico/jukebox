using jukebox.backend.InputModels;
using jukebox.backend.Persistence;
using jukebox.backend.Repositories;
using jukebox.backend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace jukebox.backend.Controllers
{
    [Route("api/autenticar")]
    [AllowAnonymous]
    public class AutenticarController : ControllerBase
    {
        private readonly JukeboxDbContext _dbContext;

        public AutenticarController(IConfiguration configuration, JukeboxDbContext dbContext)
        {
            _dbContext = dbContext;
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
                usuario = UsuarioRepository.Obter(model.Email, model.Senha);

            if (usuario == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(usuario);

            var autenticarViewModel = new AutenticarViewModel
                (
                   usuario.Nome,
                   usuario.Email,
                   usuario.Funcao
                );

            return Ok(new
            {
                usuarioAtivo = autenticarViewModel,
                token = token
            });
        }
    }
}
