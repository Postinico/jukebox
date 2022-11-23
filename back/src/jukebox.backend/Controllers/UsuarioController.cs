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
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly JukeboxDbContext _dbContext;

        public IValidator<PostUsuarioInputModel> _validador;

        public IValidator<PutUsuarioInputModel> _validadorPut;

        public UsuarioController(JukeboxDbContext dbContext, IValidator<PostUsuarioInputModel> validator, IValidator<PutUsuarioInputModel> validatorPut)
        {
            _dbContext = dbContext;
            _validador = validator;
            _validadorPut = validatorPut;
        }

        /// <summary>
        /// Obter Lista de usuário
        /// </summary>
        /// <remarks>Buscar!</remarks>
        /// <returns>lista de usuários</returns>
        [HttpGet]
        [Authorize]
        [Authorize(Roles = "gerente")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Obter()
        {
            var usuarios = _dbContext.Usuarios;

            var usuariosView = usuarios
                .Select(c => new UsuarioViewModel(c.Id, c.Nome, c.Email, c.Senha, c.Funcao))
                .ToList();

            return Ok(usuariosView);
        }

        /// <summary>
        /// Obter usuário específico
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
            var usuario = _dbContext.Usuarios.SingleOrDefault(c => c.Id == id);

            if (usuario == null) return NotFound();

            var usuarioView = new UsuarioViewModel(usuario.Id, usuario.Nome, usuario.Email, usuario.Senha, usuario.Funcao);

            return Ok(usuarioView);
        }

        /// <summary>
        /// Adicionar usuário
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
        public IActionResult Publicar([FromBody] PostUsuarioInputModel usuarioIM)
        {
            var perfil = _dbContext.Perfils.Find(usuarioIM.PerfilId);

            if (perfil == null)
                return BadRequest(new ResultViewModel(false, "Perfil não existe!", usuarioIM.PerfilId));

            if (usuarioIM == null)
                return BadRequest(new ResultViewModel(false, "Usuário não pode ser nulo!", null));

            if (!_validador.Validate(usuarioIM).IsValid)
                return BadRequest(new ResultViewModel(false, _validador.Validate(usuarioIM).Errors[0].ErrorMessage, usuarioIM));

            Usuario usuario = new Usuario(
                id: Guid.NewGuid(),
                nome: usuarioIM.Nome,
                email: usuarioIM.Email,
                senha: usuarioIM.Senha,
                funcao: perfil.Titulo,
                perfilId: usuarioIM.PerfilId
            );

            _dbContext.Usuarios.Add(usuario);

            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(Obter),
                new { id = usuario.Id },
                usuarioIM
                );
        }

        /// <summary>
        /// Alterar usuário
        /// </summary>
        /// <remarks>Alterar!</remarks>
        /// <param name="id" example="123e4567-e89b-12d3-a456-426655440000">Usuário Id</param>
        [HttpPut("{id}")]
        [Authorize]
        [Authorize(Roles = "gerente")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Alterar(Guid id, [FromBody] PutUsuarioInputModel usuarioIM)
        {
            var perfil = _dbContext.Perfils.Find(usuarioIM.PerfilId);

            if (perfil == null)
            {
                return BadRequest(new ResultViewModel(false, "Perfil não existe!", usuarioIM.PerfilId));
            }

            if (!_validadorPut.Validate(usuarioIM).IsValid)
            {
                return BadRequest(new ResultViewModel(false, _validadorPut.Validate(usuarioIM).Errors[0].ErrorMessage, usuarioIM));
            }

            var usuario = _dbContext.Usuarios
                .SingleOrDefault(c => c.Id == id);

            if (usuario == null) return NotFound();

            usuario.Update(usuarioIM.Nome, usuarioIM.Senha, usuarioIM.PerfilId);

            _dbContext.SaveChanges();

            return Accepted(new ResultViewModel(true, "Usuário alterado com sucesso!", usuarioIM.PerfilId));
        }

        /// <summary>
        /// Deletar usuario
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
            var usuario = _dbContext.Usuarios.SingleOrDefault(c => c.Id == id);

            if (usuario == null) return NotFound();

            _dbContext.Usuarios.Remove(usuario);

            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
