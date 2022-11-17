using jukebox.backend.Models;

namespace jukebox.backend.Repositories
{
    public interface IUsuarioRepository
    {
        public Usuario Obter(string email, string senha);
    }
}
