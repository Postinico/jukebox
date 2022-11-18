using FluentAssertions;
using jukeboxUnitTest.Fixture;
using Xunit;

namespace jukeboxUnitTest
{
    [Collection(nameof(GeneroColection))]
    public class GeneroTest
    {
        private readonly GeneroFixture _generoFixture;

        const string CATEGORY_NAME = "Genero";
        const string CATEGORY_VALEU = " IN ";

        public GeneroTest
        (
           GeneroFixture generoFixture
        )
        {
            _generoFixture = generoFixture;
        }

        [Fact(DisplayName = "Criar sucesso")]
        [Trait(CATEGORY_NAME, CATEGORY_VALEU)]
        public void Criar_genero_sucesso()
        {
            //Act
            var sut = _generoFixture.GeneroValido();

            //Assert
            sut.Should().NotBeNull("Objeto não pode ser nulo");
            sut.ValidationResult.Should().Be(true, "Objeto tem que ser valido");
        }

        [Theory(DisplayName = "Criar falha key")]
        [Trait(CATEGORY_NAME, CATEGORY_VALEU)]
        //[InlineAutoData(null)]
        [InlineData("")]
        [InlineData("A")]
        [InlineData("AB")]
        public void Criar_genero_falha_titulo(string titulo)
        {
            //Act
            var sut = _generoFixture.GeneroInValido(titulo: titulo);

            //Assert
            sut.Should().NotBeNull("Objeto não pode ser nulo");
            sut.ValidationResult.Should().Be(false, "Objeto tem que ser invalido");
            Assert.DoesNotContain(sut.ValidationResult.Errors, x => x.PropertyName != "Titulo");
        }
    }
}
