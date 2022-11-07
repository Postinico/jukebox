using jukebox.backend.Models;
using jukebox.backend.ViewModels;
using jukebox.backend.Persistence;
using jukebox.backend.InputModels;

using System;
using System.Net;
using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace jukebox.backend.Controllers
{
    [Route("api/musicas")]
    public class MusicaController : Controller
    {
        private readonly JukeboxDbContext _dbContext;

        public IValidator<PostMusicaInputModel> _validadorPost;

        public MusicaController(JukeboxDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Obter Lista de músicas
        /// </summary>
        /// <remarks>Buscar!</remarks>
        /// <returns>lista de músicas</returns>
        [HttpGet]
        [Authorize]
        [Authorize(Roles = "empregado, gerente")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Obter()
        {
            var musicas = _dbContext.Musicas;

            if (musicas == null)
                return NotFound();

            return Ok(musicas);
        }

        /// <summary>
        /// Obter música específica
        /// </summary>
        /// <returns>Música específica</returns>
        /// <remarks>Buscar!</remarks>
        /// <param name="id" example="123e4567-e89b-12d3-a456-426655440000">Música Id</param>
        [HttpGet("{id}")]
        [Authorize]
        [Authorize(Roles = "empregado, gerente")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult ObterId(Guid id)
        {
            var musica = _dbContext.Musicas.SingleOrDefault(c => c.Id == id);

            if (musica == null)
                return NotFound();

            return Ok(musica);
        }

        /// <summary>
        /// Obter músicas por Id Album
        /// </summary>
        /// <returns>Músicas por album Id </returns>
        /// <remarks>Buscar!</remarks>
        /// <param name="albumId" example="123e4567-e89b-12d3-a456-426655440000">Album Id</param>
        [HttpGet("obter-albumId/{albumId}")]
        [Authorize]
        [Authorize(Roles = "empregado, gerente")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult ObterAlbumId(Guid albumId)
        {
            var musicas = _dbContext.Musicas.Where(c => c.AlbumId == albumId).ToList();

            if (musicas == null)
                return NotFound();

            return Ok(musicas);
        }

        /// <summary>
        /// Adicionar música
        /// </summary>
        /// <remarks>Adicionar!</remarks>
        [HttpPost]
        [Authorize]
        [Authorize(Roles = "gerente")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Publicar([FromBody] PostMusicaInputModel musicaIM)
        {
            if (musicaIM == null)
            {
                return BadRequest(new ResultViewModel(false, "Música não pode ser nulo!", null));
            }
            if (!_validadorPost.Validate(musicaIM).IsValid)
            {
                return BadRequest(new ResultViewModel(false, _validadorPost.Validate(musicaIM).Errors[0].ErrorMessage, musicaIM));
            }

            var album = _dbContext.Albuns.Find(musicaIM.AlbumId);
            if (album != null)
            {
                var musica = new Musica(musicaIM.Nome, musicaIM.YoutubeUrl, musicaIM.AlbumId);

                _dbContext.Musicas.Add(musica);
                _dbContext.SaveChanges();

                return CreatedAtAction
                    (
                        nameof(ObterId),
                        new { id = musica.Id },
                        musicaIM
                    );
            }

            return BadRequest(new ResultViewModel(false, "Album não existe!", musicaIM.AlbumId));
        }
    }
}
