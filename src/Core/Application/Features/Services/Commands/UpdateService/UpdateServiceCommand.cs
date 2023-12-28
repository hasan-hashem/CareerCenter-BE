using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Services.Commands.UpdateService
{
    public record UpdateServiceCommand(Guid Id, string ServiceName) : IRequest;

    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateServiceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Services
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Service), request.Id);
            }

            entity.ServiceName = request.ServiceName;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }

}
