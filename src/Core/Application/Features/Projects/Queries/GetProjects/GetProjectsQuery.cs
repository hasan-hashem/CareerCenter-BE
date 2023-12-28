using Application.Common.Models;
using Application.Features.Comments.Queries.GetComments;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Application.Features.Projects.Queries.GetProjects
{
    public record GetProjectsQuery(int pageNum, int pageSize) : IRequest<(List<ProjectVm>, Pagination)>;

    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, (List<ProjectVm>, Pagination)>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProjectsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(List<ProjectVm>, Pagination)> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            //  paginaton 
            var totalItemCount = await _context.Projects.CountAsync();
            var paginationMetaData = new Pagination(totalItemCount, request.pageNum, request.pageSize);

           
            var projects = await _context.Projects
                              .OrderBy(p => p.Title)
                              .Skip(request.pageSize * (request.pageNum - 1))
                              .Take(request.pageSize)
                              .ProjectTo<ProjectVm>(_mapper.ConfigurationProvider)
                              .ToListAsync();
            return (projects, paginationMetaData);
        }
    }
}
