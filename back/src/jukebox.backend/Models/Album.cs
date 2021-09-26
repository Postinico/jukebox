using System;

namespace jukebox.backend.Models
{
    public class Album
    {
        public Album(string titulo, string descricao, string capaUrl, long generoId)
        {
            Titulo = titulo;

            Descricao = descricao;

            CapaUrl = capaUrl;

            Votos = 0;

            GeneroId = generoId;
        }

        public Guid Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string CapaUrl { get; set; }

        public int Votos { get; set; }

        public long GeneroId { get; set; }

        public Genero Genero { get; set; }

        public void Update(string titulo, string descricao, string capaUrl, int votos, long generoId)
        {
            Titulo = titulo;
            Descricao = descricao;
            Votos = votos;
            GeneroId = generoId;
        }
    }
}
