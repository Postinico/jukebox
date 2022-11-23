using FluentValidation;
using FluentValidation.Results;
using System;

namespace jukebox.backend.Models
{
    public class Musica
    {
        public Musica
        (
            Guid id,
            string nome,
            string youtubeUrl,
            Guid albumId
        )
        {
            Id = id;
            Nome = nome;
            YoutubeUrl = youtubeUrl;
            AlbumId = albumId;

            ValidationResult = ValidarDomain(this, new MusicaValidador());
        }

        public Guid Id { get; private set; }

        public Guid AlbumId { get; private set; }

        public Album Album { get; set; }

        public string Nome { get; private set; }

        public string YoutubeUrl { get; private set; }

        public int Votos { get; private set; }

        public void Update(string nome, string youtubeUrl, Guid albumId, int votos)
        {
            Nome = nome;
            YoutubeUrl = youtubeUrl;
            AlbumId = albumId;
            Votos = votos;
        }



        private ValidationResult ValidarDomain<T>(T domain, AbstractValidator<T> validator)
        {
            ValidationResult validationResult = validator.Validate(domain);

            return validationResult;
        }

        public ValidationResult ValidationResult { get; private set; }
    }

    public class MusicaValidador : AbstractValidator<Musica>
    {
        public MusicaValidador()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Informe música id");

            RuleFor(x => x.AlbumId)
                .NotNull().WithMessage("Informe album id");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Informe nome.")
                .Length(3, 50).WithMessage("Nome deve ter no mínimo 3 caracteres e no máximo 50 caracteres.");

            RuleFor(x => x.YoutubeUrl)
               .NotEmpty().WithMessage("Informe url do youtube.")
               .Length(10, 500).WithMessage("Url youtube deve ter no mínimo 10 caracteres e no máximo 50 caracteres.");
        }
    }
}
