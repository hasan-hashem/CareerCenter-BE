using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Audits.Queries.GetAudits
{
    public record GetAuditsQuery: IRequest<List<AuditVm>>;

    public class GetAuditsQueryHandler : IRequestHandler<GetAuditsQuery, List<AuditVm>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAuditsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<AuditVm>> Handle(GetAuditsQuery request, CancellationToken cancellationToken)
        {
            var audits = await _context.Auditings
                             .ProjectTo<AuditVm>(_mapper.ConfigurationProvider)
                             .ToListAsync(cancellationToken);
            return audits;

       
        }
    }

}
