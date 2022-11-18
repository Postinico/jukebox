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
            sut.Id.Should().NotBeEmpty("Id não pode ser vazio");
            sut.ValidationResult.IsValid.Should().Be(true, "Objeto tem que ser valido");
        }

        [Theory(DisplayName = "Criar falha titulo")]
        [Trait(CATEGORY_NAME, CATEGORY_VALEU)]
        [InlineData("")]
        [InlineData("A")]
        [InlineData("AB")]
        public void Criar_falha_titulo(string titulo)
        {
            //Act
            var sut = _generoFixture.GeneroInValido(titulo: titulo);

            //Assert
            sut.Should().NotBeNull("Objeto não pode ser nulo");
            sut.ValidationResult.IsValid.Should().Be(false, "Objeto tem que ser invalido");
            Assert.DoesNotContain(sut.ValidationResult.Errors, x => x.PropertyName != "Titulo");
        }
    }
}
