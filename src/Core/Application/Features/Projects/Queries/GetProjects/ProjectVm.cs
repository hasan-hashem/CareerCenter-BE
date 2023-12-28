using Application.Common.Mapping;
using Domain.Entities;

namespace Application.Features.Projects.Queries.GetProjects
{
    public class ProjectVm : IMapFrom<Project>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
