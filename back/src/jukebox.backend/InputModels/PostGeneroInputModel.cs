using FluentValidation;

namespace jukebox.backend.InputModels
{
    public class PostGeneroInputModel
    {
        public PostGeneroInputModel(string titulo)
        {
            Titulo = titulo;
        }

        /// <summary>
        /// "Titulo deve ter no mínimo 3 caracteres e no máximo 50 caracteres.
        /// </summary>
        /// <example>Eletrônica</example>
        public string Titulo { get; set; }
    }

    public class PostGeneroInputModelValidador : AbstractValidator<PostGeneroInputModel>
    {
        public PostGeneroInputModelValidador()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("Informe o título.")
                .Length(3, 50).WithMessage("Titulo deve ter no mínimo 3 caracteres e no máximo 50 caracteres.");
        }
    }
}
