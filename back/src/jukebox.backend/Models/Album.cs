using FluentValidation;
using FluentValidation.Results;
using System;

namespace jukebox.backend.Models
{
    public class Album
    {
        public Album(Guid id, string titulo, string descricao, string capaUrl, Guid generoId)
        {
            Id = id;

            Titulo = titulo;

            Descricao = descricao;

            CapaUrl = capaUrl;

            Votos = 0;

            GeneroId = generoId;

            ValidationResult = ValidarDomain(this, new AlbumValidador());
        }

        public Guid Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string CapaUrl { get; set; }

        public int Votos { get; set; }

        public Guid GeneroId { get; set; }

        public Genero Genero { get; set; }


        private ValidationResult ValidarDomain<T>(T domain, AbstractValidator<T> validator)
        {
            ValidationResult validationResult = validator.Validate(domain);

            return validationResult;
        }

        public ValidationResult ValidationResult { get; private set; }

        public void Update(string titulo, string descricao, string capaUrl, int votos, Guid generoId)
        {
            Titulo = titulo;
            Descricao = descricao;
            Votos = votos;
            GeneroId = generoId;
            CapaUrl = capaUrl;
        }
    }

    public class AlbumValidador : AbstractValidator<Album>
    {
        public AlbumValidador()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Informe o album id");

            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("Informe o título.")
                .Length(3, 50).WithMessage("Titulo deve ter no mínimo 3 caracteres e no máximo 50 caracteres.");

            RuleFor(x => x.Descricao)
               .NotEmpty().WithMessage("Informe descrição do album.")
               .Length(3, 50).WithMessage("Descrição album deve ter no mínimo 3 caracteres e no máximo 50 caracteres.");

            RuleFor(x => x.CapaUrl)
               .NotEmpty().WithMessage("Informe capa url do album.")
               .Length(3, 50).WithMessage("Cpa url album deve ter no mínimo 3 caracteres e no máximo 50 caracteres.");

            RuleFor(x => x.GeneroId) // VERIFICAR SE EXISTE NA BASE
               .NotNull().WithMessage("Informe genero Id.");
        }
    }
}
