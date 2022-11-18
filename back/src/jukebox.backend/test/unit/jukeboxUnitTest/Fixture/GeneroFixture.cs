using jukebox.backend.Models;
using System;
using Xunit;

namespace jukeboxUnitTest.Fixture
{
    [CollectionDefinition(nameof(GeneroColection))]
    public class GeneroColection : ICollectionFixture<GeneroFixture> { }

    public class GeneroFixture
    {
        public Genero GeneroValido()
        {
            return new Genero
            (
                id: Guid.NewGuid(),
                titulo: "testeUnitValido"
            );
        }

        public Genero GeneroInValido(Guid? id = null, string? titulo = null)
        {
            return new Genero
            (
                id == null ? Guid.NewGuid() : id.Value,
                titulo == null ? "testeUnitValido" : titulo
            );
        }
    }
}
