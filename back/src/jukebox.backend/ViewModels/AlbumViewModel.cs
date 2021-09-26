using System;

namespace jukebox.backend.ViewModels
{
    public class AlbumViewModel
    {
        public AlbumViewModel(Guid id, string titulo, string descricao, string capaUrl, int votos, string genero)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            CapaUrl = capaUrl;
            Votos = votos;
            Genero = genero;
        }

        public Guid Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string CapaUrl { get; set; }

        public int Votos { get; set; }

        public string Genero { get; set; }
    }
}
