using jukebox.backend.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace jukebox.backend.Persistence.Configurations
{
    public class UsuarioDbConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder
               .HasKey(c => c.Id);
        }
    }
}
