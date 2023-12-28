using Application.Features.Comments.Commands.CreateComment;
using Application.Features.Projects.Commands.CreateProject;
using Application.Features.Projects.Queries.GetProjects;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Web.Controllers
{

    public class ProjectController : ApiControllerBase
    {
        [HttpGet("project")]
        public async Task<ActionResult<List<ProjectVm>>> Get(int pageNumber, int pageSize)
        {
            var(projects, pagination) = await Mediator.Send(new GetProjectsQuery(pageNumber, pageSize));

            
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));

            return Ok(projects);
        }

        [HttpPost("project")]
        public async Task<ActionResult<Guid>> Create(CreateProjectCommand command)
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
