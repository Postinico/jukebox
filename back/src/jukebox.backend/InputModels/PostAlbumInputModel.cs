using FluentValidation;
using System;

namespace jukebox.backend.InputModels
{
    public class PostAlbumInputModel
    {
        /// <summary>
        /// "Titulo deve ter no mínimo 3 caracteres e no máximo 50 caracteres.
        /// </summary>
        /// <example>Cabaré</example>
        public string Titulo { get; set; }

        /// <summary>
        /// Descrição deve ter no mínimo 3 caracteres e no máximo 100 caracteres.
        /// </summary>
        /// <example>AO VIVO EM SALVADOR</example>
        public string Descricao { get; set; }

        /// <summary>
        /// Necessário fornecer imagens .JPG.
        /// </summary>
        /// <example>../../../assets/images/gabe.jpg</example>
        public string CapaUrl { get; set; }

        /// <summary>
        /// Genero Id Guid
        /// </summary>
        /// <example>23e4567-e89b-12d3-a456-426655440000</example>
        public long GeneroId { get; set; }
    }

    public class PostAlbumInputModelValidador : AbstractValidator<PostAlbumInputModel>
    {
        public PostAlbumInputModelValidador()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("Informe o título.")
                .Length(3, 50).WithMessage("Titulo deve ter no mínimo 3 caracteres e no máximo 50 caracteres.");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Informe a Descrição.")
                .Length(3, 100).WithMessage("Descrição deve ter no mínimo 3 caracteres e no máximo 100 caracteres.");

            RuleFor(x => x.CapaUrl)
               .NotEmpty().WithMessage("Informe a capa url.")
               .Length(3, 100).WithMessage("Capa url deve ter no mínimo 3 caracteres e no máximo 100 caracteres.");

            RuleFor(x => x.GeneroId)
                .NotEmpty().WithMessage("Informe o genero Id.");
        }
    }
}
