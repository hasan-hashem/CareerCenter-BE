using Domain.Common;

namespace Domain.Entities
{
    public class Project : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<Offer> Offers { get; set; } = new List<Offer>();
    }
}
