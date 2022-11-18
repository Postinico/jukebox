using FluentValidation;
using jukebox.backend.InputModels;
using jukebox.backend.Models;
using jukebox.backend.Persistence;
using jukebox.backend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace jukebox.backend.Controllers
{
    [Route("api/generos")]
    public class GeneroController : Controller
    {
        private readonly JukeboxDbContext _dbContext;

        public IValidator<PostGeneroInputModel> _validador;

        public GeneroController(JukeboxDbContext dbContext, IValidator<PostGeneroInputModel> validador)
        {
            _dbContext = dbContext;
            _validador = validador;
        }

        /// <summary>
        /// Obter lista de gênero
        /// </summary>
        /// <remarks>Buscar!</remarks>
        [HttpGet]
        [Authorize]
        [Authorize(Roles = "empregado, gerente")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Obter()
        {
            var generos = _dbContext.Generos;

            var generosView = generos
                .Select(c => new GeneroViewModel(c.Id, c.Titulo))
                .ToList();

            return Ok(generosView);
        }

        /// <summary>
        /// Obter gênero específico
        /// </summary>
        /// <remarks>Buscar!</remarks>
        [HttpGet("{id}")]
        [Authorize]
        [Authorize(Roles = "empregado, gerente")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult ObterId(Guid id)
        {
            var genero = _dbContext.Generos.SingleOrDefault(c => c.Id == id);

            if (genero == null) return NotFound();

            var generoView = new GeneroViewModel(genero.Id, genero.Titulo);

            return Ok(generoView);
        }

        /// <summary>
        /// Adicionar gênero
        /// </summary>
        /// <remarks>Adicionar!</remarks>
        [HttpPost]
        [Authorize]
        [Authorize(Roles = "gerente")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Publicar([FromBody] PostGeneroInputModel generoVM)
        {
            if (!_validador.Validate(generoVM).IsValid)
            {
                return BadRequest(new ResultViewModel(false, _validador.Validate(generoVM).Errors[0].ErrorMessage, generoVM));
            }

            Genero genero = new Genero(Guid.NewGuid(), generoVM.Titulo);

            _dbContext.Generos.Add(genero);

            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(ObterId),
                new { id = genero.Id },
                generoVM
                );
        }

        /// <summary>
        /// Alterar gênero
        /// </summary>
        /// <remarks>Alterar!</remarks>
        [HttpPut("{id}")]
        [Authorize]
        [Authorize(Roles = "empregado, gerente")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Alterar(Guid id, [FromBody] PostGeneroInputModel generoVM)
        {
            var genero = _dbContext.Generos
                .SingleOrDefault(c => c.Id == id);

            if (genero == null)
            {
                return NotFound(new ResultViewModel(false, "Gênero não localizado!", id));
            }

            genero.Update(generoVM.Titulo);

            _dbContext.SaveChanges();

            return Accepted(new ResultViewModel(true, "Gênero alterado com sucesso!", generoVM));
        }

        /// <summary>
        /// Deletar gênero
        /// </summary>
        /// <remarks>Deletar!</remarks>
        [HttpDelete("{id}")]
        [Authorize]
        [Authorize(Roles = "gerente")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Excluir(Guid id)
        {
            var genero = _dbContext.Generos.SingleOrDefault(c => c.Id == id);

            if (genero == null)
            {
                return NotFound(new ResultViewModel(false, "Gênero não localizado!", id));
            }
            else
            {
                _dbContext.Remove(genero);
                _dbContext.SaveChanges();

                return NoContent();
            }
        }
    }
}
