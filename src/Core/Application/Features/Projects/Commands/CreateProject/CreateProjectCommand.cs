using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Projects.Commands.CreateProject
{
    public record CreateProjectCommand: IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class CreateProjectCommandHandler: IRequestHandler<CreateProjectCommand,Guid>
    {

        private readonly IApplicationDbContext _context;

        public CreateProjectCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var entity = new Project();
            entity.UserId = request.UserId;
            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.CreatedDate = request.CreatedDate;

            _context.Projects.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}

