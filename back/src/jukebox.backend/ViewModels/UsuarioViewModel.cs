using System;

namespace jukebox.backend.ViewModels
{
    public class UsuarioViewModel
    {
        public UsuarioViewModel(Guid usuarioId, string nome, string email, string senha, string funcao)
        {
            UsuarioId = usuarioId;
            Nome = nome;
            Email = email;
            Senha = senha;
            Funcao = funcao;
        }

        public Guid UsuarioId { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string Funcao { get; set; }
    }
}
