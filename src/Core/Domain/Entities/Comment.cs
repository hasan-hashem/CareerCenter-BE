using Domain.Common;

namespace Domain.Entities
{
    public class Comment : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
