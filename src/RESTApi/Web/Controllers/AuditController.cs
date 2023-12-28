using Application.Features.Audits.Commands.CreateAudit;
using Application.Features.Audits.Queries.GetAudits;
using Application.Features.Comments.Commands.CreateComment;
using Application.Features.Comments.Queries.GetComments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{

    public class AuditController : ApiControllerBase
    {
        [HttpGet("auditing")]
        public async Task<ActionResult<List<AuditVm>>> Get()
        {
            return await Mediator.Send(new GetAuditsQuery());
        }

        [HttpPost("auditing")]
        public async Task<ActionResult<Guid>> Create(CreateAuditCommand command)
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
