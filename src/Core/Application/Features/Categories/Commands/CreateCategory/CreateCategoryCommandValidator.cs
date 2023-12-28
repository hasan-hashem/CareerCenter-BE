using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator: AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.CategoryName)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).
                    WithMessage("Title must not exceed 200 characters.");
        }
    }
}
