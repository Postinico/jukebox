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
    [Route("api/perfilusuarios")]
    public class UsuarioPerfilController : ControllerBase
    {
        private readonly JukeboxDbContext _dbContext;

        public IValidator<PostUsuarioPerfilInputModel> _validador;

        public UsuarioPerfilController(JukeboxDbContext dbContext, IValidator<PostUsuarioPerfilInputModel> validador)
        {
            _dbContext = dbContext;
            _validador = validador;
        }

        /// <summary>
        /// Obter lista de perfil
        /// </summary>
        /// <remarks>Buscar!</remarks>
        [HttpGet]
        [Authorize]
        [Authorize(Roles = "gerente")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Obter()
        {
            var perfils = _dbContext.Perfils;

            var perfilsView = perfils
                .Select(c => new UsuarioPerfilViewModel(c.PerfilId, c.Titulo))
                .ToList();

            return Ok(perfilsView);
        }

        /// <summary>
        /// Obter perfil específico
        /// </summary>
        /// <remarks>Buscar!</remarks>
        [HttpGet("{id}")]
        [Authorize]
        [Authorize(Roles = "gerente")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult ObterId(Guid id)
        {
            var perfil = _dbContext.Perfils.SingleOrDefault(c => c.PerfilId == id);

            if (perfil == null) return NotFound();

            var perfilView = new UsuarioPerfilViewModel(perfil.PerfilId, perfil.Titulo);

            return Ok(perfilView);
        }

        /// <summary>
        /// Adicionar perfil
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
        public IActionResult Publicar([FromBody] PostUsuarioPerfilInputModel perfilIM)
        {
            if (!_validador.Validate(perfilIM).IsValid)
                return BadRequest(new ResultViewModel(false, _validador.Validate(perfilIM).Errors[0].ErrorMessage, perfilIM));

            UsuarioPerfil perfil = new();

            perfil.Titulo = perfilIM.Titulo;

            _dbContext.Perfils.Add(perfil);

            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(ObterId),
                new { id = perfil.PerfilId },
                perfilIM
                );
        }

        /// <summary>
        /// Alterar perfil
        /// </summary>
        /// <remarks>Alterar!</remarks>
        [HttpPut("{id}")]
        [Authorize]
        [Authorize(Roles = "gerente")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Alterar(Guid id, [FromBody] PostGeneroInputModel perfilIM)
        {
            var perfil = _dbContext.Perfils
                .SingleOrDefault(c => c.PerfilId == id);

            if (perfil == null)
                return NotFound(new ResultViewModel(false, "Perfil não localizado!", id));

            perfil.Update(perfilIM.Titulo);

            _dbContext.SaveChanges();

            return Accepted(new ResultViewModel(true, "Perfil alterado com sucesso!", perfilIM));
        }

        /// <summary>
        /// Deletar perfil
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
            var perfil = _dbContext.Perfils.SingleOrDefault(c => c.PerfilId == id);

            if (perfil == null)
                return NotFound(new ResultViewModel(false, "Perfil não localizado!", id));

            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
