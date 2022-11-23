using Bogus;
using jukebox.backend.Models;
using System;
using Xunit;

namespace jukeboxUnitTest.Fixture
{
    [CollectionDefinition(nameof(MusicaColection))]
    public class MusicaColection : ICollectionFixture<MusicaFixture> { }

    public class MusicaFixture
    {
        public Musica MusicaValido()
        {
            var musica = new Faker<Musica>("pt_BR")
                 .CustomInstantiator(f => new Musica
                 (
                     id: Guid.NewGuid(),
                     albumId: Guid.NewGuid(),
                     nome: f.Random.String(30),
                     youtubeUrl: f.Random.String(30)
                  ));

            return musica;
        }

        public Musica MusicaInValido
        (
           Guid? id = null,
           Guid? albumId = null,
           string? nome = null,
           string? youtubeUrl = null
        )
        {
            return new Musica
            (
                id == null ? Guid.NewGuid() : id.Value,
                nome == null ? "testeUnitValido" : nome,
                youtubeUrl == null ? "testeUnitValido" : youtubeUrl,
                albumId == null ? Guid.NewGuid() : albumId.Value
            );
        }
    }
}
