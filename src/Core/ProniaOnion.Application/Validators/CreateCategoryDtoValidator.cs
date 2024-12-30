using FluentValidation;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.DTOs.Categories;

namespace ProniaOnion.Application.Validators
{
    internal class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        private readonly ICategoryRepository _repository;

        public CreateCategoryDtoValidator(ICategoryRepository repository)
        {
            _repository = repository;

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Data required.")
                .MaximumLength(100).WithMessage("Characters should be less than 100")
                .Matches(@"^[A-Za-z\s0-9]*$").WithMessage("Invalid characters or length")
                .MustAsync(CheckNameExistence).WithMessage("Category name already exists."); ;
        }

        public async Task<bool> CheckNameExistence(string name, CancellationToken token)
        {
            return !await _repository.AnyAsync(c => c.Name == name);
        }
    }
}
