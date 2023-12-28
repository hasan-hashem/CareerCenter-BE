using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Audits.Commands.CreateAudit
{
    public record CreateAuditCommand: IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class CreateAuditCommandHandler : IRequestHandler<CreateAuditCommand, Guid>
    {

        private readonly IApplicationDbContext _context;

        public CreateAuditCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateAuditCommand request, CancellationToken cancellationToken)
        {

            var entity = new Auditing();

            entity.UserId = request.UserId;
            entity.Status = request.Status;
            entity.Message = request.Message;
            entity.Url = request.Url;
            entity.CreatedDate = request.CreatedDate;

            _context.Auditings.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }

}
