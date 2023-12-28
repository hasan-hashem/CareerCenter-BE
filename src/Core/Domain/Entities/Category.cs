using Domain.Common;

namespace Domain.Entities
{
    public class Category : BaseEntity<Guid>
    {
        public string CategoryName { get; set; } = string.Empty;
        public bool IsJob { get; set; }
        public List<Service> Services { get; set; } = new List<Service>();
        public List<Job> Jobs { get; set; } = new List<Job>();

    }
}
