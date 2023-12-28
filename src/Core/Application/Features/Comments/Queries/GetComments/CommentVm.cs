using Application.Common.Mapping;
using Domain.Entities;

namespace Application.Features.Comments.Queries.GetComments
{
    public class CommentVm: IMapFrom<Comment>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
