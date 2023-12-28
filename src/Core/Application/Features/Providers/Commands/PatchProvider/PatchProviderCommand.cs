using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Providers.Commands.PatchProvider
{
    public record PatchProviderCommand(Guid id, JsonPatchDocument<ConnectionStatusVm> PatchDocument) : IRequest;

    public class PatchProviderCommandHandler : IRequestHandler<PatchProviderCommand>
    {
        private readonly IApplicationDbContext _context;

        public PatchProviderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(PatchProviderCommand request, CancellationToken cancellationToken)
        {
            var provider = await _context.Providers.FirstOrDefaultAsync(p => p.Id == request.id);

            var providerToPatch = new ConnectionStatusVm()
            {
                IsDeleted = provider.IsDeleted
            };

            request.PatchDocument.ApplyTo(providerToPatch);

            provider.IsDeleted = providerToPatch.IsDeleted;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }


}
