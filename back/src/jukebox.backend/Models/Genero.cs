using FluentValidation;
using FluentValidation.Results;
using System;

namespace jukebox.backend.Models
{
    public class Genero : ValidationResult
    {
        public Genero() { }
        public Genero(Guid id, string titulo)
        {
            Id = id;
            Titulo = titulo;

            CheckInvariants(this, new GeneroValidador());
        }

        public Guid Id { get; private set; }

        public string Titulo { get; private set; }
        public virtual bool Valid { get; private set; }

        public void Update(string titulo)
        {
            Titulo = titulo;
        }

        protected bool CheckInvariants<T>(T aggregate, AbstractValidator<T> validator)
        {
            ValidationResult validationResult = validator.Validate(aggregate);
            for (int j = 0; j < validationResult.Errors.Count; j++)
            {
                ValidationResult.Errors.Add(validationResult.Errors[j]);
            }

            Valid = ValidationResult.IsValid;
            if (!Valid)
            {
                return false;
            }

            return true;
        }

        public virtual ValidationResult ValidationResult { get; private set; }
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
