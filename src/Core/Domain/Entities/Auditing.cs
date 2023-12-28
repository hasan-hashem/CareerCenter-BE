using Domain.Common;

namespace Domain.Entities
{
    public class Auditing : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
