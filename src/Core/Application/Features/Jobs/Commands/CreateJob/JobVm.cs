namespace Application.Features.Jobs.Commands.CreateJob
{
    public class JobVm
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public bool IsStoped { get; set; }
        public string Url { get; set; }
        //public Guid CategoryId { get; set; }
    }
}
