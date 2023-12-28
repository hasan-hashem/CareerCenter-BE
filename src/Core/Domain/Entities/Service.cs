using Domain.Common;

namespace Domain.Entities
{
    public class Service : BaseEntity<Guid>
    {
        public string ServiceName { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<Provider> Providers { get; set; } = new List<Provider>();
    }
}
