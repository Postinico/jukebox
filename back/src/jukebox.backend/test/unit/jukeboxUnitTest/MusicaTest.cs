using FluentAssertions;
using jukeboxUnitTest.Fixture;
using Xunit;

namespace jukeboxUnitTest
{
    [Collection(nameof(MusicaColection))]
    public class MusicaTest
    {
        private readonly MusicaFixture _musicaFixture;

        const string CATEGORY_NAME = "Música";
        const string CATEGORY_VALEU = " IN ";

        public MusicaTest
        (
           MusicaFixture musicaFixture
        )
        {
            _musicaFixture = musicaFixture;
        }

        [Fact(DisplayName = "Criar sucesso")]
        [Trait(CATEGORY_NAME, CATEGORY_VALEU)]
        public void Criar_musica_sucesso()
        {
            //Act
            var sut = _musicaFixture.MusicaValido();

            //Assert
            sut.Should().NotBeNull("Objeto não pode ser nulo");
            sut.Id.Should().NotBeEmpty("Id não pode ser vazio");
            sut.AlbumId.Should().NotBeEmpty("Id não pode ser vazio");
            sut.ValidationResult.IsValid.Should().Be(true, "Objeto tem que ser valido");
        }

        [Theory(DisplayName = "Criar falha nome")]
        [Trait(CATEGORY_NAME, CATEGORY_VALEU)]
        [InlineData("")]
        [InlineData("A")]
        [InlineData("AB")]
        [InlineData("adadadadadaddaddddadadadadadadadadadadaddadadadadadad")]
        public void Criar_falha_nome(string nome)
        {
            //Act
            var sut = _musicaFixture.MusicaInValido(nome: nome);

            //Assert
            sut.Should().NotBeNull("Objeto não pode ser nulo");
            sut.ValidationResult.IsValid.Should().Be(false, "Objeto tem que ser invalido");
            Assert.DoesNotContain(sut.ValidationResult.Errors, x => x.PropertyName != "Nome");
        }

        [Theory(DisplayName = "Criar falha youtube Url")]
        [Trait(CATEGORY_NAME, CATEGORY_VALEU)]
        [InlineData("")]
        [InlineData("A")]
        [InlineData("AB")]
        [InlineData("adadadadadaddaddddadadadadadadadadadadaddadadadadadad" +
            "adadadadadaddaddddadadadadadadadadadadaddadadadadadad" +
            "adadadadadaddaddddadadadadadadadadadadaddadadadadadad" +
            "adadadadadaddaddddadadadadadadadadadadaddadadadadadad" +
            "adadadadadaddaddddadadadadadadadadadadaddadadadadadad" +
            "adadadadadaddaddddadadadadadadadadadadaddadadadadadad" +
            "adadadadadaddaddddadadadadadadadadadadaddadadadadadad" +
            "adadadadadaddaddddadadadadadadadadadadaddadadadadadad" +
            "adadadadadaddaddddadadadadadadadadadadaddadadadadadad" +
            "adadadadadaddaddddadadadadadadadadadadaddadadadadadad" +
            "adadadadadaddaddddadadadadadadadadadadaddadadadadadad")]
        public void Criar_falha_youtube_url(string youtubeUrl)
        {
            //Act
            var sut = _musicaFixture.MusicaInValido(youtubeUrl: youtubeUrl);

            //Assert
            sut.Should().NotBeNull("Objeto não pode ser nulo");
            sut.ValidationResult.IsValid.Should().Be(false, "Objeto tem que ser invalido");
            Assert.DoesNotContain(sut.ValidationResult.Errors, x => x.PropertyName != "YoutubeUrl");
        }
    }
}
