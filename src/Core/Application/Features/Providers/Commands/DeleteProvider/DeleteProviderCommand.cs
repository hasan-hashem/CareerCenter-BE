using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Providers.Commands.DeleteProvider
{
    public record DeleteProviderCommand(Guid id) : IRequest;
    public class DeleteProviderCommandHandler : IRequestHandler<DeleteProviderCommand>
    {

        private readonly IApplicationDbContext _context;

        public DeleteProviderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteProviderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Providers
            .Where(p => p.Id == request.id)
            .SingleOrDefaultAsync(cancellationToken);

            if(entity != null) {
                _context.Providers.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new NotFoundException(nameof(Provider), request.id);
            }

            return Unit.Value;
        }
    }

}
