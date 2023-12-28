using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Services.Commands.CreateService
{
    public record CreateServiceCommand(Guid id, string ServiceName) : IRequest<Guid>;

    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateServiceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            var entity = new Service();

            bool exists = await _context.Services.AnyAsync(s => s.ServiceName == request.ServiceName);
            if (!exists) {
                entity.ServiceName = request.ServiceName;
                entity.CategoryId = request.id;

                _context.Services.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
            return request.id;
        }
    }
}
