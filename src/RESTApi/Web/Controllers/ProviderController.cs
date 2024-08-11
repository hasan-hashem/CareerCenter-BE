using Application.Features.Providers.Commands.CreateProvider;
using Application.Features.Providers.Commands.DeleteProvider;
using Application.Features.Providers.Commands.PatchProvider;
using Application.Features.Providers.Commands.UpdateProvider;
using Application.Features.Providers.Queries.GetProviders;
using Application.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.IdentityModel.Tokens;
using Persistence.Identity;
using Persistence.Models;

namespace Web.Controllers
{
    public class ProviderController : ApiControllerBase
    {
        private readonly IApplicationDbContext _context;
        private readonly IAuthService _authService;  // إضافة خدمة إدارة الأدوار

        public ProviderController(IApplicationDbContext context, IAuthService authService)  // تضمين خدمة إدارة الأدوار
        {
            _context = context;
            _authService = authService;
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
        public async Task<ActionResult<Guid>> Create(Guid id, [FromBody] ProviderDto command)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Service ID cannot be empty.");
            }

            if (command == null)
            {
                return BadRequest("Provider data is required.");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(m => m.Value.Errors.Count > 0)
                                       .Select(m => new
                                       {
                                           Field = m.Key,
                                           Errors = m.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                                       }).ToArray();

                return BadRequest(new { errors });
            }

            try
            {
                //var isInRole = await _authService.IsUserInRoleAsync(command.UserId, command.)
                //if (isInRole)
                //{
                //    return BadRequest("User already has the provider role.");
                //}

                var result = await Mediator.Send(new CreateProviderCommand(id, command));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
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
