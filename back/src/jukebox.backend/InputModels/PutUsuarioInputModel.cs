using FluentValidation;
using System;

namespace jukebox.backend.InputModels
{
    public class PutUsuarioInputModel
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public Guid PerfilId { get; set; }
    }

    public class PutUsuarioInputModelValidador : AbstractValidator<PutUsuarioInputModel>
    {
        public PutUsuarioInputModelValidador()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Informe o nome")
                .Length(3, 50).WithMessage("Nome deve ter no mínimo 3 caracteres e no máximo 50 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Informe o Email")
                .EmailAddress().WithMessage("Informe um Email válido");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("Informe a senha")
                .Length(3, 10).WithMessage("Senha deve ter no mínimo 3 caracteres e no máximo 10 caracteres.");
        }
    }
}
