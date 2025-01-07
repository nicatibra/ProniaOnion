using FluentValidation;
using ProniaOnion.Application.DTOs.Products;

namespace ProniaOnion.Application.Validators.ProductDtoValidator
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public const int MAX_NAME_LENGTH = 100;

        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(MAX_NAME_LENGTH).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than 0.")
                .LessThanOrEqualTo(999999.99m).WithMessage("Price must not exceed 1000000.");

            RuleFor(x => x.SKU)
                .NotEmpty().WithMessage("SKU is required.")
                .MinimumLength(3).WithMessage("SKU must be at least 3 characters.")
                .MaximumLength(10).WithMessage("SKU must not exceed 50 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category is required.")
                .GreaterThan(0).WithMessage("Category Id must be greater than 0.");

            RuleForEach(x => x.ColorIds)
                .NotEmpty().WithMessage("Color is required.")
                .Must(colorId => colorId > 0).WithMessage("Color Ids must be greater than 0.");

            RuleFor(c => c.ColorIds)
                .NotEmpty().WithMessage("Color is required.")
                .Must(colorId => colorId.Count > 0).WithMessage("Choose at least 1 color.");

            RuleForEach(x => x.SizeIds)
                .NotEmpty().WithMessage("Size is required.")
                .Must(sizeId => sizeId > 0).WithMessage("Size Ids must be greater than 0.");

            RuleFor(s => s.SizeIds)
                .NotEmpty().WithMessage("Size is required.")
                .Must(sizeId => sizeId.Count > 0).WithMessage("Choose at least 1 size.");

            RuleForEach(x => x.TagIds)
                .NotEmpty().WithMessage("Tag is required.")
                .Must(tagId => tagId > 0).WithMessage("Tag Ids must be greater than 0.");

            RuleFor(t => t.TagIds)
                .NotEmpty().WithMessage("Tag is required.")
                .Must(tagId => tagId.Count > 0).WithMessage("Choose at least 1 tag.");
        }
    }
}
