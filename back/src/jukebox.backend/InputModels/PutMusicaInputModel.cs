using FluentValidation;

namespace jukebox.backend.InputModels
{
    public class PutMusicaInputModel
    {
        /// <summary>
        /// Nome deve ter no mínimo 3 caracteres e no máximo 120 caracteres.
        /// </summary>
        /// <example>Cabaré</example>
        public string Nome { get; set; }

        /// <summary>
        /// YoutubeUrl deve ter no mínimo 3 caracteres e no máximo 200 caracteres.
        /// </summary>
        /// <example>https://www.youtube.com/?hl=pt</example>
        public string YoutubeUrl { get; set; }

        /// <summary>
        /// Album Id Guid
        /// </summary>
        /// <example>23e4567-e89b-12d3-a456-426655440000</example>
        public System.Guid AlbumId { get; set; }

        /// <summary>
        /// vostos int
        /// </summary>
        /// <example>23e4567-e89b-12d3-a456-426655440000</example>
        public int Votos { get; set; }
    }

    public class PutMusicaInputModelValidador : AbstractValidator<PutMusicaInputModel>
    {
        public PutMusicaInputModelValidador()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Informe Nome.")
                .Length(3, 50).WithMessage("Nome deve ter no mínimo 3 caracteres e no máximo 120 caracteres.");

            RuleFor(x => x.YoutubeUrl)
               .NotEmpty().WithMessage("Informe url do Youtube.")
               .Length(3, 200).WithMessage("Youtube url deve ter no mínimo 3 caracteres e no máximo 200 caracteres.");

            RuleFor(x => x.AlbumId)
                .NotEmpty().WithMessage("Informe album Id.");
        }
    }
}
