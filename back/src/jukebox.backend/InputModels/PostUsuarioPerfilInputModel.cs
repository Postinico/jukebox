using FluentValidation;

namespace jukebox.backend.InputModels
{
    public class PostUsuarioPerfilInputModel
    {
        public PostUsuarioPerfilInputModel(string titulo)
        {
            Titulo = titulo;
        }

        /// <summary>
        /// "Titulo deve ter no mínimo 3 caracteres e no máximo 50 caracteres.
        /// </summary>
        /// <example>gerente</example>
        public string Titulo { get; set; }
    }

    public class PostUsuarioPerfilInputModelValidador : AbstractValidator<PostUsuarioPerfilInputModel>
    {
        public PostUsuarioPerfilInputModelValidador()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("Informe o título.")
                .Length(3, 50).WithMessage("Titulo deve ter no mínimo 3 caracteres e no máximo 50 caracteres.");
        }
    }
}
