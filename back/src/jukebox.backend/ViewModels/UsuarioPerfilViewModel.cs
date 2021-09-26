using System;

namespace jukebox.backend.ViewModels
{
    public class UsuarioPerfilViewModel
    {
        public UsuarioPerfilViewModel(Guid id, string titulo)
        {
            Id = id;
            Titulo = titulo;
        }

        public Guid Id { get; set; }

        public string Titulo { get; set; }
    }
}
