using Domain.Common;

namespace Domain.Entities
{
    public class Provider : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public string AliasName { get; set; }
        public string Skills { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public bool IsConnected { get; set; }
        public bool IsDeleted { get; set; }
        public string DurationTime { get; set; }
        public string ServiceMode { get; set; }
        public string Phone { get; set; }
        public string Telegram { get; set; }

        public Guid ServiceId { get; set; }
        public Service Service { get; set; }

    }
}
