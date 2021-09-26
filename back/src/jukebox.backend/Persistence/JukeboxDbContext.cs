using jukebox.backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace jukebox.backend.Persistence
{
    public class JukeboxDbContext : DbContext
    {
        public JukeboxDbContext(DbContextOptions<JukeboxDbContext> options) : base(options) { }

        public DbSet<Album> Albuns { get; set; }

        public DbSet<Genero> Generos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<UsuarioPerfil> Perfils { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
