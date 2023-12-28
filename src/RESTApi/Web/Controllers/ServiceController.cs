using Application.Features.Services.Commands.CreateService;
using Application.Features.Services.Commands.DeleteService;
using Application.Features.Services.Commands.UpdateService;
using Application.Features.Services.Queries.GetServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ServiceController : ApiControllerBase
    {

        [HttpGet("service")]
        public async Task<ActionResult<List<ServiceVm>>> GetAll()
        {
            return await Mediator.Send(new GetServicesQuery());
        }

        [HttpGet("category/{id}/service")]
        public async Task<ActionResult<List<ServiceVm>>> GetById(Guid id)
        {
            return await Mediator.Send(new GetItemServicesQuery(id));
        }

        [HttpPost("category/{id}/service")]
        public async Task<ActionResult<Guid>> Create(Guid id, ServiceCommandVm command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =  await Mediator.Send(new CreateServiceCommand(id, command.ServiceName));
            return (result == id) ? BadRequest("("+command.ServiceName + ") already exists") : result;
        }

        [HttpPut("service/{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] ServiceCommandVm command)
        {
            if (!ModelState.IsValid)
            {       
                return BadRequest(ModelState);
            }

            await Mediator.Send(new UpdateServiceCommand(id, command.ServiceName));

            return NoContent();
        }

        [HttpDelete("service/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await Mediator.Send(new DeleteServiceCommand(id));

            return NoContent();
        }
    }
}
