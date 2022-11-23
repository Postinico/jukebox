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
                     id: Guid.NewGuid(),
                     titulo: f.Random.String(30),
                     descricao: f.Random.String(30),
                     capaUrl: f.Random.String(30),
                     generoId: f.Random.Guid()
                  ));

            return album;
        }

        public Album AlbumInValido
        (
            Guid? id = null,
            string? titulo = null,
            string? descricao = null,
            string? capaUrl = null,
            Guid? generoId =  null
        )
        {
            return new Album
            (
                id == null ? Guid.NewGuid() : id.Value,
                titulo == null ? "testeUnitValido" : titulo,
                descricao == null ? "testeUnitValido" : descricao,
                capaUrl == null ? "testeUnitValido" : capaUrl,
                generoId == null ? Guid.NewGuid() : generoId.Value
            );
        }
    }
}
