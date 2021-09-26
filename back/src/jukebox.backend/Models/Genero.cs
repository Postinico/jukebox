namespace jukebox.backend.Models
{
    public class Genero
    {
        public long Id { get; set; }

        public string Titulo { get; set; }

        public void Update(string titulo)
        {
            Titulo = titulo;
        }
    }
}
