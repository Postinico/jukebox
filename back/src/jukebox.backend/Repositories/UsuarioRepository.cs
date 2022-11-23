using jukebox.backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace jukebox.backend.Repositories
{
    public static class UsuarioRepository
    {
        public static Usuario Obter(string email, string senha)
        {
            var usuarios = new List<Usuario>();

            usuarios.Add(new Usuario
                (
                    id:Guid.NewGuid(),
                    perfilId:Guid.NewGuid(),
                    nome:"Guilherme Postinico",
                    email:"postinico15@gmail.com", 
                    senha:"123456", 
                    funcao:"gerente"
                ));

            var usuario = usuarios.Where
                (x =>
                    x.Email.ToLower() == email.ToLower() &&
                    x.Senha.ToLower() == senha.ToLower()
                ).FirstOrDefault();

            return usuario;
        }
    }
}
