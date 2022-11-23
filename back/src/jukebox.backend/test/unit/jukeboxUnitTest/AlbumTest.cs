using FluentAssertions;
using jukeboxUnitTest.Fixture;
using Xunit;

namespace jukeboxUnitTest
{
    [Collection(nameof(AlbumColection))]
    public class AlbumTest
    {
        private readonly AlbumFixture _albumFixture;

        const string CATEGORY_NAME = "Genero";
        const string CATEGORY_VALEU = " IN ";

        public AlbumTest
        (
           AlbumFixture albumFixture
        )
        {
            _albumFixture = albumFixture;
        }

        [Fact(DisplayName = "Criar sucesso")]
        [Trait(CATEGORY_NAME, CATEGORY_VALEU)]
        public void Criar_album_sucesso()
        {
            //Act
            var sut = _albumFixture.AlbumValido();

            //Assert
            sut.Should().NotBeNull("Objeto não pode ser nulo");
            sut.Id.Should().NotBeEmpty("Id não pode ser vazio");
            sut.GeneroId.Should().NotBeEmpty("Id não pode ser vazio");
            sut.ValidationResult.IsValid.Should().Be(true, "Objeto tem que ser valido");
        }

        [Theory(DisplayName = "Criar falha titulo")]
        [Trait(CATEGORY_NAME, CATEGORY_VALEU)]
        [InlineData("")]
        [InlineData("A")]
        [InlineData("AB")]
        [InlineData("adadadadadaddaddddadadadadadadadadadadaddadadadadadad")]
        public void Criar_falha_titulo(string titulo)
        {
            //Act
            var sut = _albumFixture.AlbumInValido(titulo: titulo);

            //Assert
            sut.Should().NotBeNull("Objeto não pode ser nulo");
            sut.ValidationResult.IsValid.Should().Be(false, "Objeto tem que ser invalido");
            Assert.DoesNotContain(sut.ValidationResult.Errors, x => x.PropertyName != "Titulo");
        }

        [Theory(DisplayName = "Criar falha descrição")]
        [Trait(CATEGORY_NAME, CATEGORY_VALEU)]
        [InlineData("")]
        [InlineData("A")]
        [InlineData("AB")]
        [InlineData("adadadadadaddaddddadadadadadadadadadadaddadadadadadad")]
        public void Criar_falha_descricao(string descricao)
        {
            //Act
            var sut = _albumFixture.AlbumInValido(descricao: descricao);

            //Assert
            sut.Should().NotBeNull("Objeto não pode ser nulo");
            sut.ValidationResult.IsValid.Should().Be(false, "Objeto tem que ser invalido");
            Assert.DoesNotContain(sut.ValidationResult.Errors, x => x.PropertyName != "Descricao");
        }

        [Theory(DisplayName = "Criar falha capa url")]
        [Trait(CATEGORY_NAME, CATEGORY_VALEU)]
        [InlineData("")]
        [InlineData("A")]
        [InlineData("AB")]
        [InlineData("adadadadadaddaddddadadadadadadadadadadaddadadadadadad")]
        public void Criar_falha_capa_url(string capaUrl)
        {
            //Act
            var sut = _albumFixture.AlbumInValido(capaUrl: capaUrl);

            //Assert
            sut.Should().NotBeNull("Objeto não pode ser nulo");
            sut.ValidationResult.IsValid.Should().Be(false, "Objeto tem que ser invalido");
            Assert.DoesNotContain(sut.ValidationResult.Errors, x => x.PropertyName != "CapaUrl");
        }
    }
}
