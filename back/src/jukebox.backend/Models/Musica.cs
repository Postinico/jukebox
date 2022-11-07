using System;

namespace jukebox.backend.Models
{
    public class Musica
    {
        public Musica
        (
            string nome,
            string youtubeUrl,
            Guid albumId
        )
        {
            Nome = nome;
            YoutubeUrl = youtubeUrl;
            AlbumId = albumId;
        }

        public Guid Id { get; set; }

        public Guid AlbumId { get; set; }

        public string Nome { get; set; }

        public string YoutubeUrl { get; set; }

        public int Votos { get; set; }
    }
}
