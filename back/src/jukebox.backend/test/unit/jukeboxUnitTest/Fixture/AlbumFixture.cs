using Bogus;
using jukebox.backend.Models;
using System;
using Xunit;

namespace jukeboxUnitTest.Fixture
{
    [CollectionDefinition(nameof(AlbumColection))]
    public class AlbumColection : ICollectionFixture<AlbumFixture> { }

    public class AlbumFixture
    {
        public Album AlbumValido()
        {
            var album = new Faker<Album>("pt_BR")
                 .CustomInstantiator(f => new Album
                 (
                     titulo: f.Music.Random.String(),
                     descricao: f.Music.Random.String(),
                     capaUrl: f.Music.Random.String(),
                     generoId: f.Music.Random.Guid()
                  ));

            return album;
        }

        public Album AlbumInValido
        (
            string? titulo = null,
            string? descricao = null,
            string? capaUrl = null,
            Guid? generoId =  null
        )
        {
            return new Album
            (
                titulo == null ? "testeUnitValido" : titulo,
                descricao == null ? "testeUnitValido" : descricao,
                capaUrl == null ? "testeUnitValido" : capaUrl,
                generoId == null ? Guid.NewGuid() : generoId.Value
            );
        }
    }
}
