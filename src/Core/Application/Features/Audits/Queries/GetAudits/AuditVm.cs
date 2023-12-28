using Application.Common.Mapping;
using Domain.Entities;

namespace Application.Features.Audits.Queries.GetAudits
{
    public class AuditVm : IMapFrom<Auditing>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
