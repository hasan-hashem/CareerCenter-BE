using Application.Features.Comments.Commands.CreateComment;
using Application.Features.Comments.Queries.GetComments;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class CommentController : ApiControllerBase
    {
        [HttpGet("comment")]
        public async Task<ActionResult<List<CommentVm>>> Get()
        {
            return await Mediator.Send(new GetCommentsQuery());
        }

        [HttpPost("comment")]
        public async Task<ActionResult<Guid>> Create(CreateCommentCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(command);

            return result;
        }
    }
}
