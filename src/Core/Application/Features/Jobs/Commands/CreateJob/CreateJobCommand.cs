using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Jobs.Commands.CreateJob
{
    public record CreateJobCommand(Guid categoryId, JobVm Job) : IRequest<Guid>;

    public class CreateJobCommandHandler: IRequestHandler<CreateJobCommand,Guid>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateJobCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Job>(request.Job);
            entity.CategoryId = request.categoryId;
            _context.Jobs.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }

}
