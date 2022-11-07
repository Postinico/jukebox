using jukebox.backend.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace jukebox.backend.Persistence.Configurations
{
    public class PerfilDbConfiguration : IEntityTypeConfiguration<UsuarioPerfil>
    {
        public void Configure(EntityTypeBuilder<UsuarioPerfil> builder)
        {
            builder
                .HasKey(c => c.PerfilId);
        }
    }
}
