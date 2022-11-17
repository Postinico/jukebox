using jukebox.backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace jukebox.backend.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public Usuario Obter(string email, string senha)
        {
            var usuarios = new List<Usuario>();

            usuarios.Add(new Usuario { UsuarioId = Guid.NewGuid(), Email = "postinico15@gmail.com", Nome = "Guilherme Postinico", Senha = "123456", Funcao = "gerente" });

            var usuario = usuarios.Where
                (x =>
                    x.Email.ToLower() == email.ToLower() &&
                    x.Senha.ToLower() == senha.ToLower()
                ).FirstOrDefault();

            return usuario;
        }
    }
}
