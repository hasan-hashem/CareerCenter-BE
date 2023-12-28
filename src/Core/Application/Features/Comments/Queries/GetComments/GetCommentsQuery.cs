using Application.Features.Services.Queries.GetServices;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Comments.Queries.GetComments
{
    public class GetCommentsQuery : IRequest<List<CommentVm>>;

    public class GetCommentQueryHandler: IRequestHandler<GetCommentsQuery, List<CommentVm>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCommentQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CommentVm>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var top10Comments = await _context.Comments
                                         .OrderByDescending(c => c.CreatedDate)
                                         .Take(10)
                                         .ProjectTo<CommentVm>(_mapper.ConfigurationProvider)
                                         .ToListAsync();
            return top10Comments;
        }
    }
}
