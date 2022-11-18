using FluentValidation;
using FluentValidation.Results;
using System;

namespace jukebox.backend.Models
{
    public class Genero
    {
        protected Genero() { }

        public Genero(Guid id, string titulo)
        {
            Id = id;
            Titulo = titulo;

            ValidationResult = ValidarDomain(this, new GeneroValidador());
        }

        public Guid Id { get; private set; }

        public string Titulo { get; private set; }

        public void Update(string titulo)
        {
            Titulo = titulo;
        }

        private ValidationResult ValidarDomain<T>(T domain, AbstractValidator<T> validator)
        {
            ValidationResult validationResult = validator.Validate(domain);

            return validationResult;
        }
        
        public ValidationResult ValidationResult { get; private set; }
    }

    public class GeneroValidador : AbstractValidator<Genero>
    {
        public GeneroValidador()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Informe o Genero id");

            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("Informe o título.")
                .Length(3, 50).WithMessage("Titulo deve ter no mínimo 3 caracteres e no máximo 50 caracteres.");
        }
    }
}
