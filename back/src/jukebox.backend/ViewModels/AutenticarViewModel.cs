namespace jukebox.backend.ViewModels
{
    public class AutenticarViewModel
    {
        public AutenticarViewModel(string nome, string email, string funcao)
        {
            Nome = nome;
            Email = email;
            Funcao = funcao;
        }

        public string Nome { get; private set; }

        public string Email { get; private set; }

        public string Funcao { get; private set; }
    }
}
