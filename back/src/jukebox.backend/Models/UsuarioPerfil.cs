using System;

namespace jukebox.backend.Models
{
    public class UsuarioPerfil
    {
        public Guid PerfilId { get; set; }

        public string Titulo { get; set; }

        public void Update(string titulo)
        {
            Titulo = titulo;
        }
    }
}
