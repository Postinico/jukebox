using System;

namespace jukebox.backend.ViewModels
{
    public class MusicaViewModel
    {
        public MusicaViewModel
        (
            Guid id,
            string nome,
            string youtubeUrl,
            string album
        )
        {
            Id = id;
            Nome = nome;
            YoutubeUrl = youtubeUrl;
            Album = album;
        }

        public Guid Id { get; private set; }

        public string Album { get; private set; }

        public string Nome { get; private set; }

        public string YoutubeUrl { get; private set; }

        public int Votos { get; private set; }
    }
}
