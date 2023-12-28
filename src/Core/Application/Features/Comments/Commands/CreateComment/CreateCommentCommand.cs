using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Comments.Commands.CreateComment
{
    public record CreateCommentCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public record CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Guid>
    {

        private readonly IApplicationDbContext _context;

        public CreateCommentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {

                var entity = new Comment();

                entity.Name = request.Name;
                entity.Email = request.Email;
                entity.Text = request.Text;
                entity.CreatedDate = request.CreatedDate;

                 _context.Comments.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
  
        }
    }
}
