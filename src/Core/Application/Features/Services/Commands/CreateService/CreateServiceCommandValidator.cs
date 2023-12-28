using Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Services.Commands.CreateService
{
    public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateServiceCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(s => s.ServiceName)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.")
                .MustAsync(BeUniqueService)
                        .WithMessage("The specified title already exists.")
                        .WithErrorCode("Unique"); 
        }

        public async Task<bool> BeUniqueService(string service, CancellationToken cancellationToken)
        {
            return await _context.Services
                .AllAsync(s => s.ServiceName != service, cancellationToken);
        }
    }
}
