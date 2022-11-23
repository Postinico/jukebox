using System;

namespace jukebox.backend.Models
{
    public class Usuario
    {
        public Usuario
        (
            Guid id,
            Guid perfilId,
            string nome, 
            string email,
            string senha,
            string funcao
        )
        {
            Id = id;
            PerfilId = perfilId;
            Nome = nome;
            Email = email;
            Senha = senha;
            Funcao = funcao;
        }

        public Guid Id { get; private set; }

        public Guid PerfilId { get; private set; }

        public string Nome { get; private set; }

        public string Email { get; private set; }

        public string Senha { get; private set; }

        public string Funcao { get; private set; }

        public void Update(string nome, string senha, Guid perfiId)
        {
            Nome = nome;
            Senha = senha;
            PerfilId = perfiId;
        }
    }
}
