using Application.Features.Jobs.Commands.CreateJob;
using Application.Features.Jobs.Queries.GetJobs;
using Application.Features.Providers.Commands.CreateProvider;
using Application.Features.Providers.Queries.GetProviders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
  
    public class JobController : ApiControllerBase
    {
        
        [HttpGet("job")]
        public async Task<ActionResult<List<JobQueryVm>>> Get()
        {
            return await Mediator.Send(new GetJobsQuery());
        }

        [HttpPost("category/{id}/job")]
        public async Task<ActionResult<Guid>> Create(Guid id, JobVm command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new CreateJobCommand(id, command));

            return result;
        }
    }
}
