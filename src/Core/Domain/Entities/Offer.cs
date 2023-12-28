using Domain.Common;

namespace Domain.Entities
{
    public class Offer : BaseEntity<Guid>
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid UserId { get; set; }
        public string Comment { get; set; }
        public int StartRating { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
