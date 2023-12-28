using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Providers.Queries.GetProviders
{
    public record GetProvidersByUserIdQuery(Guid id) : IRequest<List<ProviderVm>>;

    public class GetProvidersByUserIdQueryHandler : IRequestHandler<GetProvidersByUserIdQuery, List<ProviderVm>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProvidersByUserIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProviderVm>> Handle(GetProvidersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var providers = await _context.Providers.Where(p => p.UserId == request.id)
                                          .ProjectTo<ProviderVm>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);
            return providers;
        }
    }
}
