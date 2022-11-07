using jukebox.backend.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace jukebox.backend.Persistence.Configurations
{
    public class MusicaDbConfiguration : IEntityTypeConfiguration<Musica>
    {
        public void Configure(EntityTypeBuilder<Musica> builder)
        {
            builder
               .HasKey(c => c.Id);
        }
    }
}
