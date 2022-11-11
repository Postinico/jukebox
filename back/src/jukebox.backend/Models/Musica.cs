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

        public Guid Id { get; private set; }

        public Guid AlbumId { get; private set; }

        public string Nome { get; private set; }

        public string YoutubeUrl { get; private set; }

        public int Votos { get; private set; }

        public void Update(string nome, string youtubeUrl, Guid albumId, int votos)
        {
            Nome = nome;
            YoutubeUrl = youtubeUrl;
            AlbumId = albumId;
            Votos = votos;
        }
    }
}
