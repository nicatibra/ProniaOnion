using FluentValidation;
using ProniaOnion.Application.DTOs.Blogs;

namespace ProniaOnion.Application.Validators
{
    public class CreateBlogDtoValidator : AbstractValidator<CreateBlogDto>
    {

        public CreateBlogDtoValidator()
        {

            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Data required.")
                .MaximumLength(100).WithMessage("Characters should be less than 100")
                .Matches(@"^[A-Za-z\s0-9]*$").WithMessage("Invalid characters or length");
            //.MustAsync(CheckNameExistence).WithMessage("Blog name already exists.");

            RuleFor(b => b.Article)
                .NotEmpty().WithMessage("Data required.")
                .MaximumLength(200).WithMessage("Characters should be less than 200")
                .Matches(@"^[A-Za-z\s0-9]*$").WithMessage("Invalid characters or length");
        }
    }
}
