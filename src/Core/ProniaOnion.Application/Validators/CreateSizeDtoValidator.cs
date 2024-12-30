using FluentValidation;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.DTOs.Sizes;

namespace ProniaOnion.Application.Validators
{
    public class CreateSizeDtoValidator : AbstractValidator<CreateSizeDto>
    {
        private readonly ISizeRepository _repository;

        public CreateSizeDtoValidator(ISizeRepository repository)
        {
            _repository = repository;

            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("Data required.")
                .MaximumLength(100).WithMessage("Characters should be less than 100")
                .Matches(@"^[A-Za-z\s0-9]*$").WithMessage("Invalid characters or length");
            //.MustAsync(CheckNameExistence).WithMessage("Size name already exists.");
        }

        public async Task<bool> CheckNameExistence(string name, CancellationToken token)
        {
            return !await _repository.AnyAsync(s => s.Name == name);
        }
    }
}
