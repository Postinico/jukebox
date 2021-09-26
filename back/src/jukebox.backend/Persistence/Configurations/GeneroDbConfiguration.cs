using jukebox.backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace jukebox.backend.Persistence.Configurations
{
    public class GeneroDbConfiguration : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder
                .HasKey(c => c.Id);
        }
    }
}
