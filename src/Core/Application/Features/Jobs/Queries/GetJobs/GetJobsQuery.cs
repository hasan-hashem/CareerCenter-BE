using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Jobs.Queries.GetJobs
{
    public record GetJobsQuery : IRequest<List<JobQueryVm>>;

    public class GetJobsQueryHandler : IRequestHandler<GetJobsQuery, List<JobQueryVm>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetJobsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<JobQueryVm>> Handle(GetJobsQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _context.Jobs
                             .ProjectTo<JobQueryVm>(_mapper.ConfigurationProvider)
                             .ToListAsync(cancellationToken);
            return jobs;
        }
    }
}
