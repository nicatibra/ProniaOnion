using FluentValidation;
using ProniaOnion.Application.DTOs.Genres;

namespace ProniaOnion.Application.FluentValidator.GenreDtoValidator
{
    public class CreateGenreDtoValidator : AbstractValidator<CreateGenreDto>
    {
        public CreateGenreDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Data required.")
                .MaximumLength(100).WithMessage("Characters should be less than 100")
                .Matches(@"^[A-Za-z\s0-9]*$").WithMessage("Invalid characters or length");
        }
    }
}
