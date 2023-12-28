using Application.Features.Providers.Commands.CreateProvider;
using Application.Features.Providers.Commands.DeleteProvider;
using Application.Features.Providers.Commands.PatchProvider;
using Application.Features.Providers.Commands.UpdateProvider;
using Application.Features.Providers.Queries.GetProviders;
using Application.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{

    public class ProviderController : ApiControllerBase
    {
        private readonly IApplicationDbContext _context;

        public ProviderController(IApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("provider")]
        public async Task<ActionResult<List<ProviderVm>>> GetAll()
        {
            return await Mediator.Send(new GetProvidersQuery());
        }

        [HttpGet("provider/user/{id}")]
        public async Task<ActionResult<List<ProviderVm>>> GetByUser(Guid id)
        {
            return await Mediator.Send(new GetProvidersByUserIdQuery(id));
        }

        [HttpPost("service/{id}/provider")]
        public async Task<ActionResult<Guid>> Create(Guid id, ProviderDto command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new CreateProviderCommand(id, command));
            return result;
        }

        [HttpPut("provider/{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] ProviderForUpdate command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await Mediator.Send(new UpdateProviderCommand(id, command));

            return NoContent();
        }

        [HttpDelete("provider/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await Mediator.Send(new DeleteProviderCommand(id));

            return NoContent();
        }
        [HttpPatch("provider/{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<ConnectionStatusVm> PatchDocument)
        {
            await Mediator.Send(new PatchProviderCommand(id, PatchDocument));

            return NoContent();
        }
    }
}
