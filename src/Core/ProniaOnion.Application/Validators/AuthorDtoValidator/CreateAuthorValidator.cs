using FluentValidation;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.DTOs.Authors;

namespace ProniaOnion.Application.Validators.AuthorDtoValidator
{
    public class CreateAuthorVaolidator : AbstractValidator<CreateAuthorDto>
    {
        private readonly IAuthorRepository _repository;

        public CreateAuthorVaolidator(IAuthorRepository repository)
        {
            _repository = repository;

            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Data required.")
                .MaximumLength(30).WithMessage("Characters should be less than 30")
                .Matches(@"^[a-zA-Z'-]{3,30}$").WithMessage("Invalid characters or length");
            //.MustAsync(CheckNameExistence).WithMessage("Author name already exists.");

            RuleFor(a => a.Surname)
                .NotEmpty().WithMessage("Data required.")
                .MaximumLength(30).WithMessage("Characters should be less than 30")
                .Matches(@"^[a-zA-Z'-]{3,30}$").WithMessage("Invalid characters or length");
            //.MustAsync(CheckNameExistence).WithMessage("Author name already exists.");
        }

        public async Task<bool> CheckNameExistence(string name, CancellationToken token)
        {
            return !await _repository.AnyAsync(a => a.Name == name);
        }
    }
}
