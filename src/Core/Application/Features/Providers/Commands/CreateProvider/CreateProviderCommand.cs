using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Providers.Commands.CreateProvider
{
    public record CreateProviderCommand(Guid serviceId, ProviderDto provider) : IRequest<Guid>;

    public class CreateProviderCommandHandler : IRequestHandler<CreateProviderCommand, Guid>
    {
        // Create a field to store the mapper object
        
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateProviderCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(CreateProviderCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Provider>(request.provider);
            entity.ServiceId = request.serviceId;
            _context.Providers.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;

        }
    }
}
