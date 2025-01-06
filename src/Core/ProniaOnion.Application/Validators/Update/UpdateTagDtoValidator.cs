using FluentValidation;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.DTOs.Tags;

namespace ProniaOnion.Application.Validators.Update
{
    public class UpdateTagDtoValidator : AbstractValidator<UpdateTagDto>
    {
        private readonly ITagRepository _repository;

        public UpdateTagDtoValidator(ITagRepository repository)
        {
            _repository = repository;

            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("Data required.")
                .MaximumLength(100).WithMessage("Characters should be less than 100")
                .Matches(@"^[A-Za-z\s0-9]*$").WithMessage("Invalid characters or length");
            //.MustAsync(CheckNameExistence).WithMessage("Tag name already exists."); 
        }

        public async Task<bool> CheckNameExistence(string name, CancellationToken token)
        {
            return !await _repository.AnyAsync(t => t.Name == name);
        }
    }

}
