using System;

namespace jukebox.backend.Models
{
    public class Usuario
    {
        public Guid UsuarioId { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string Funcao { get; set; }

        public Guid PerfilId { get; set; }

        public void Update(string nome, string senha, Guid perfiId)
        {
            Nome = nome;
            Senha = senha;
            PerfilId = perfiId;
        }
    }
}
