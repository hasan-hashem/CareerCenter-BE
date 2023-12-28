using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Providers.Queries.GetProviders
{
    public record GetProvidersQuery : IRequest<List<ProviderVm>>;

    public class GetProvidersQueryHandler : IRequestHandler<GetProvidersQuery, List<ProviderVm>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProvidersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ProviderVm>> Handle(GetProvidersQuery request, CancellationToken cancellationToken)
        {
            var providers = await _context.Providers
                                          .ProjectTo<ProviderVm>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);
            return providers;
        }
    }

}
