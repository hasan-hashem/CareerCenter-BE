namespace Application.Features.Jobs.Queries.GetJobs
{
    public class JobQueryVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public bool IsStoped { get; set; }
        public string Url { get; set; }
        public Guid CategoryId { get; set; }
    }
}
