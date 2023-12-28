using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Application.Interfaces;

namespace Application.Features.Services.Queries.GetServices
{
    public record GetServicesQuery : IRequest<List<ServiceVm>>;
    public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, List<ServiceVm>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetServicesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ServiceVm>> Handle(GetServicesQuery request, CancellationToken cancellationToken)
        {
            var services = await _context.Services
                            .ProjectTo<ServiceVm>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);
            return services;
        }
    }
}
