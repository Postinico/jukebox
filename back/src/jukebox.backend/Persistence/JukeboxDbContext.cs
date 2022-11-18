using jukebox.backend.Models;

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using FluentValidation.Results;

namespace jukebox.backend.Persistence
{
    public class JukeboxDbContext : DbContext
    {
        public JukeboxDbContext(DbContextOptions<JukeboxDbContext> options) : base(options) { }

        public DbSet<Album> Albuns { get; set; }

        public DbSet<Genero> Generos { get; set; }

        public DbSet<Musica> Musicas { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<UsuarioPerfil> Perfils { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
