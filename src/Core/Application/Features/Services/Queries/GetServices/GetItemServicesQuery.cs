using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Application.Interfaces;


namespace Application.Features.Services.Queries.GetServices
{
    public record GetItemServicesQuery(Guid Id) : IRequest<List<ServiceVm>>;

    public class GetItemServicesQueryHandler : IRequestHandler<GetItemServicesQuery, List<ServiceVm>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetItemServicesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ServiceVm>> Handle(GetItemServicesQuery request, CancellationToken cancellationToken)
        {
            var services = await _context.Services.Where(s => s.CategoryId == request.Id)
                           .ProjectTo<ServiceVm>(_mapper.ConfigurationProvider)
                           .ToListAsync(cancellationToken);
            return services;
        }
    }
}
