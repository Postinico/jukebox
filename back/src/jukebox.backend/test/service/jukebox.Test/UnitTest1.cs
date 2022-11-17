using AutoFixture;
using jukebox.backend.InputModels;
using Moq;

namespace jukebox.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // arrange
            var album = new Fixture().Create<PostAlbumInputModel>();

            //
        }
    }
}