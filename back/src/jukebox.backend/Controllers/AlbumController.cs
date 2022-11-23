using FluentValidation;
using jukebox.backend.InputModels;
using jukebox.backend.Models;
using jukebox.backend.Persistence;
using jukebox.backend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;

namespace jukebox.backend.Controllers
{
    [Route("api/albuns")]
    public class AlbumController : Controller
    {
        private readonly JukeboxDbContext _dbContext;

        public IValidator<PostAlbumInputModel> _validadorPost;

        public IValidator<PutAlbumInputModel> _validadorPut;

        public AlbumController(JukeboxDbContext dbContext, IValidator<PostAlbumInputModel> validadorPost, IValidator<PutAlbumInputModel> validadorPut)
        {
            _dbContext = dbContext;
            _validadorPost = validadorPost;
            _validadorPut = validadorPut;
        }

        /// <summary>
        /// Obter Lista de album
        /// </summary>
        /// <remarks>Buscar!</remarks>
        /// <returns>lista de album</returns>
        [HttpGet]
        [Authorize]
        [Authorize(Roles = "empregado, gerente")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Obter()
        {
            var albuns = _dbContext.Albuns
                 .Include(f => f.Genero);

            if (albuns == null)
                return NotFound();

            var albumViewModel = albuns
                .Select(c => new AlbumViewModel(c.Id, c.Titulo, c.Descricao, c.CapaUrl, c.Votos, c.Genero.Titulo))
                .ToList();

            Console.WriteLine(String.Format("Autenticado - {0}", User.Identity.Name));
            Console.WriteLine(String.Format("Autenticado - {0}", User.Identity.Name));
            return Ok(albumViewModel);
        }

        /// <summary>
        /// Obter album específico
        /// </summary>
        /// <returns>Album específico</returns>
        /// <remarks>Buscar!</remarks>
        /// <param name="id" example="123e4567-e89b-12d3-a456-426655440000">Album Id</param>
        [HttpGet("{id}")]
        [Authorize]
        [Authorize(Roles = "empregado, gerente")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult ObterId(Guid id)
        {
            var album = _dbContext.Albuns.SingleOrDefault(c => c.Id == id);

            if (album == null)
                return NotFound();

            var albumViewModel = new AlbumViewModel(
                album.Id,
                album.Titulo,
                album.Descricao,
                album.CapaUrl,
                album.Votos,
                album.Genero.Titulo
                );

            return Ok(albumViewModel);
        }

        /// <summary>
        /// Obter albuns por Id Genero
        /// </summary>
        /// <returns>Albuns por genero Id </returns>
        /// <remarks>Buscar!</remarks>
        /// <param name="generoId" example="011">Genero Id</param>
        [HttpGet("obter-generoid/{generoId}")]
        [Authorize]
        [Authorize(Roles = "empregado, gerente")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult ObterGeneroId(Guid generoId)
        {
            var albuns = _dbContext.Albuns.Where(c => c.GeneroId == generoId).Include(f => f.Genero).ToList();

            if (albuns == null)
                return NotFound();


            var albumViewModel = albuns
                .Select(c => new AlbumViewModel(c.Id, c.Titulo, c.Descricao, c.CapaUrl, c.Votos, c.Genero.Titulo))
                .ToList();
            //String.Format("Autenticado - {0}", User.Identity.Name)
            return Ok(albumViewModel);
        }

        /// <summary>
        /// Adicionar album
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
        public IActionResult Publicar([FromBody] PostAlbumInputModel albumIM)
        {
            if (albumIM == null)
            {
                return BadRequest(new ResultViewModel(false, "Album não pode ser nulo!", null));
            }
            if (!_validadorPost.Validate(albumIM).IsValid)
            {
                return BadRequest(new ResultViewModel(false, _validadorPost.Validate(albumIM).Errors[0].ErrorMessage, albumIM));
            }

            var genero = _dbContext.Generos.Find(albumIM.GeneroId);

            if (genero != null)
            {
                var album = new Album(Guid.NewGuid(), albumIM.Titulo, albumIM.Descricao, albumIM.CapaUrl, albumIM.GeneroId);

                _dbContext.Albuns.Add(album);

                _dbContext.SaveChanges();

                return CreatedAtAction(
                    nameof(ObterId),
                    new { id = album.Id },
                    albumIM
                    );
            }

            return BadRequest(new ResultViewModel(false, "Gênero não existe!", albumIM.GeneroId));
        }

        /// <summary>
        /// Alterar album
        /// </summary>
        /// <remarks>Alterar!</remarks>
        /// <param name="id" example="123e4567-e89b-12d3-a456-426655440000">Album Id</param>
        [HttpPut("{id}")]
        [Authorize]
        [Authorize(Roles = "empregado, gerente")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Alterar(Guid id, [FromBody] PutAlbumInputModel albumIM)
        {
            if (!_validadorPut.Validate(albumIM).IsValid)
            {
                return BadRequest(new ResultViewModel(false, _validadorPut.Validate(albumIM).Errors[0].ErrorMessage, albumIM));
            }


            var album = _dbContext.Albuns
                .SingleOrDefault(c => c.Id == id);

            if (album == null)
                return NotFound();

            var genero = _dbContext.Generos.Find(albumIM.GeneroId);

            if (genero != null)
            {
                album.Update(albumIM.Titulo, albumIM.Descricao, albumIM.CapaUrl, albumIM.Votos, albumIM.GeneroId);

                _dbContext.SaveChanges();

                return Accepted(new ResultViewModel(true, "Album alterado com sucesso!", albumIM.GeneroId));
            }

            return BadRequest(new ResultViewModel(false, "Album não existe!", albumIM.GeneroId));
        }

        /// <summary>
        /// Deletar album
        /// </summary>
        /// <remarks>Deletar!</remarks>
        [HttpDelete("{id}")]
        [Authorize]
        [Authorize(Roles = "gerente")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult Excluir(Guid id)
        {
            var album = _dbContext.Albuns.SingleOrDefault(c => c.Id == id);

            if (album == null)
                return NotFound();

            _dbContext.Albuns.Remove(album);

            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
