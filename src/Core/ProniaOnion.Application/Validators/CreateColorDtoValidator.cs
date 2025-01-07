using FluentValidation;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.DTOs.Colors;

namespace ProniaOnion.Application.Validators
{
    public class CreateColorDtoValidator : AbstractValidator<CreateColorDto>
    {
        private readonly IColorRepository _repository;

        public CreateColorDtoValidator(IColorRepository repository)
        {
            _repository = repository;

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Data required.")
                .MaximumLength(100).WithMessage("Characters should be less than 100")
                .Matches(@"^[A-Za-z\s0-9]*$").WithMessage("Invalid characters or length");
            //.MustAsync(CheckNameExistence).WithMessage("Color name already exists.");
        }

        public async Task<bool> CheckNameExistence(string name, CancellationToken token)
        {
            return !await _repository.AnyAsync(c => c.Name == name);
        }
    }
}
