using jukebox.backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace jukebox.backend.Persistence.Configurations
{
    public class AlbumDbConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder
               .HasKey(c => c.Id);
        }
    }
}
