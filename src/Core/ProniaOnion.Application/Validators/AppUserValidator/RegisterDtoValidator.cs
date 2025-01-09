using FluentValidation;
using ProniaOnion.Application.DTOs.AppUsers;

namespace ProniaOnion.Application.Validators.AppUserValidator
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters")
                .MaximumLength(50).WithMessage("Name can not exceed 50 characters")
                .Matches("^[a-zA-Z ]*$").WithMessage("Name must contain only letters");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Surname is required")
                .MinimumLength(3).WithMessage("Surname must be at least 3 characters")
                .MaximumLength(50).WithMessage("Surname can not exceed 50 characters")
                .Matches("^[a-zA-Z ]*$").WithMessage("Surname must contain only letters");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters")
                .MaximumLength(256).WithMessage("Username can not exceed 50 characters")
                .Matches("^[a-zA-Z0-9-._@+]*$").WithMessage("Username must contain only letters, numbers and -._@+");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid")
                .MaximumLength(256).WithMessage("Email can not exceed 256 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .MaximumLength(50).WithMessage("Password can not exceed 50 characters")
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,50}$")
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number and one special character");
        }
    }
}
