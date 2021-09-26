namespace jukebox.backend.ViewModels
{
    public class GeneroViewModel
    {
        public GeneroViewModel(long id, string titulo)
        {
            Id = id;
            Titulo = titulo;
        }

        public long Id { get; set; }

        public string Titulo { get; set; }
    }
}
